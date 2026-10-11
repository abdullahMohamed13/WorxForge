using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WorxForge.API.Configuration;
using WorxForge.API.EndpointGroups;
using WorxForge.API.Middlewares;
using WorxForge.Core.Abstractions;
using WorxForge.Core.Configuration;
using WorxForge.Core.Handlers.Assets;
using WorxForge.Core.Validators.Assets;
using WorxForge.Domain.Commands.Assets;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Infrastructure.Options;

namespace WorxForge.API.UnitTests;

public sealed class EndpointHardeningTests
{
    private sealed class TestApp(WebApplication app, HttpClient client, Mock<ICommandHandler<UploadMarkdownFileCommand, UploadMarkdownResponse>> uploads) : IAsyncDisposable
    {
        public HttpClient Client { get; } = client;
        public Mock<ICommandHandler<UploadMarkdownFileCommand, UploadMarkdownResponse>> Uploads { get; } = uploads;
        public async ValueTask DisposeAsync() { Client.Dispose(); await app.DisposeAsync(); }
    }

    private static async Task<TestApp> StartAsync(bool guestAccess = true, int uploadPermits = 10)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = "Production" });
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["GuestAccess:Enabled"] = guestAccess.ToString(),
            ["JwtAccessToken:Issuer"] = "test-issuer",
            ["JwtAccessToken:Audience"] = "test-audience",
            ["JwtAccessToken:SigningKey"] = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
            ["JwtAccessToken:ExpirationMinutes"] = "60",
            ["Uploads:PermitLimit"] = uploadPermits.ToString()
        });
        builder.Services.Configure<JwtAccessTokenOptions>(builder.Configuration.GetSection("JwtAccessToken"));
        builder.Services.AddWorxForgeAuthentication(builder.Configuration);
        builder.Services.AddWorxForgeAuthorization();
        builder.Services.AddWorxForgeUploadProtection(builder.Configuration);
        builder.Services.AddWorxForgeCore();
        var uploads = new Mock<ICommandHandler<UploadMarkdownFileCommand, UploadMarkdownResponse>>();
        var realHandler = new UploadMarkdownFileCommandHandler(new UploadMarkdownFileCommandValidator());
        uploads.Setup(handler => handler.HandleAsync(It.IsAny<UploadMarkdownFileCommand>(), It.IsAny<CancellationToken>()))
            .Returns((UploadMarkdownFileCommand command, CancellationToken token) => realHandler.HandleAsync(command, token));
        builder.Services.AddSingleton(uploads.Object);
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        var app = builder.Build();
        app.Urls.Add("http://127.0.0.1:0");
        app.UseExceptionHandler();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRateLimiter();
        app.UseMiddleware<RequestBodyLimitMiddleware>();
        app.MapAdminEndpoints();
        app.MapAssetsEndpoints();
        app.MapGet("/ping", () => "ok");
        await app.StartAsync();
        var address = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.Single();
        return new TestApp(app, new HttpClient { BaseAddress = new Uri(address) }, uploads);
    }

    private static async Task<HttpResponseMessage> UploadAsync(HttpClient client, byte[] bytes, string filename = "asset.md", bool chunked = false, bool expectContinue = false)
    {
        using var form = new MultipartFormDataContent();
        form.Add(new ByteArrayContent(bytes), "file", filename);
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/assets/upload-markdown") { Content = form };
        if (chunked) request.Headers.TransferEncodingChunked = true;
        request.Headers.ExpectContinue = expectContinue;
        return await client.SendAsync(request);
    }

    [Theory]
    [InlineData(true, HttpStatusCode.Forbidden)]
    [InlineData(false, HttpStatusCode.Unauthorized)]
    public async Task EveryAdminRoute_RejectsGuestsOrAnonymousUsersBeforeBinding(bool guestAccess, HttpStatusCode expected)
    {
        await using var app = await StartAsync(guestAccess);
        var id = Guid.NewGuid();
        foreach (var (method, path) in new[]
        {
            (HttpMethod.Get, "/users"), (HttpMethod.Post, "/users"), (HttpMethod.Delete, $"/users/{id}"),
            (HttpMethod.Get, "/assets"), (HttpMethod.Delete, $"/assets/{id}"),
            (HttpMethod.Get, "/comments"), (HttpMethod.Delete, $"/comments/{id}"),
            (HttpMethod.Get, "/reports"), (HttpMethod.Put, $"/reports/{id}")
        })
        {
            using var request = new HttpRequestMessage(method, "/api/admin" + path) { Content = JsonContent.Create(new { }) };
            using var response = await app.Client.SendAsync(request);
            Assert.Equal(expected, response.StatusCode);
        }
    }

    [Fact]
    public async Task MarkdownFile_AtLimitIsAccepted_AndAboveLimitNeverReachesHandler()
    {
        await using var app = await StartAsync();
        var bytes = Enumerable.Repeat((byte)'a', AssetValidationConstants.MarkdownFileMaxSizeBytes).ToArray();
        using var accepted = await UploadAsync(app.Client, bytes);
        Assert.Equal(HttpStatusCode.OK, accepted.StatusCode);
        var result = await accepted.Content.ReadFromJsonAsync<UploadMarkdownResponse>();
        Assert.Equal(bytes.Length, result!.MarkdownContent.Length);
        app.Uploads.Invocations.Clear();
        using var rejected = await UploadAsync(app.Client, new byte[bytes.Length + 1]);
        Assert.Equal(HttpStatusCode.BadRequest, rejected.StatusCode);
        Assert.Empty(app.Uploads.Invocations);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task OversizedRequest_IsRejectedBeforeHandler_WithOrWithoutContentLength(bool chunked)
    {
        await using var app = await StartAsync();
        // The known-length request must be rejected on its headers, before any body is sent.
        try
        {
            using var response = await UploadAsync(app.Client, new byte[6 * 1024 * 1024], chunked: chunked, expectContinue: !chunked);
            if (chunked)
                Assert.Contains(response.StatusCode, new[] { HttpStatusCode.RequestEntityTooLarge, HttpStatusCode.BadRequest });
            else
                Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
        }
        catch (HttpRequestException exception) when (chunked)
        {
            // Kestrel can reset a violating chunked connection while the client is still writing.
            // Accept only that transport rejection, never a refused/unreachable server or other error.
            var cause = exception.InnerException;
            while (cause?.InnerException is not null) cause = cause.InnerException;
            Assert.Equal(SocketError.ConnectionReset, Assert.IsType<SocketException>(cause).SocketErrorCode);
        }
        Assert.Empty(app.Uploads.Invocations);
        Assert.Equal("ok", await app.Client.GetStringAsync("/ping"));
    }

    [Fact]
    public async Task MultipleFiles_AreRejectedBeforeHandler()
    {
        await using var app = await StartAsync();
        using var form = new MultipartFormDataContent();
        form.Add(new ByteArrayContent("# One"u8.ToArray()), "file", "one.md");
        form.Add(new ByteArrayContent("# Two"u8.ToArray()), "file", "two.md");
        using var response = await app.Client.PostAsync("/api/assets/upload-markdown", form);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Empty(app.Uploads.Invocations);
    }

    [Fact]
    public async Task UploadRateLimit_Returns429AndRetryAfter_WithoutBlockingOtherRoutes()
    {
        await using var app = await StartAsync(uploadPermits: 2);
        for (var i = 0; i < 2; i++)
        {
            using var allowed = await UploadAsync(app.Client, "# Valid markdown"u8.ToArray());
            Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);
        }
        app.Uploads.Invocations.Clear();
        app.Client.DefaultRequestHeaders.Add("X-Forwarded-For", "203.0.113.123");
        using var rejected = await UploadAsync(app.Client, "# Valid markdown"u8.ToArray());
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);
        Assert.True(rejected.Headers.RetryAfter?.Delta > TimeSpan.Zero);
        Assert.Empty(app.Uploads.Invocations);
        Assert.Equal("ok", await app.Client.GetStringAsync("/ping"));
    }

    [Theory]
    [InlineData("asset.txt", "# Valid")]
    [InlineData("empty.md", "")]
    public async Task ExistingFileValidation_RemainsInEffect(string filename, string content)
    {
        await using var app = await StartAsync();
        using var response = await UploadAsync(app.Client, System.Text.Encoding.UTF8.GetBytes(content), filename);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
