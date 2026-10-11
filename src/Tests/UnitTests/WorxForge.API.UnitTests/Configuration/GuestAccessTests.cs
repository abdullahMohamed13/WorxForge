using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WorxForge.API.Configuration;
using WorxForge.API.EndpointGroups;
using WorxForge.Core.Abstractions;
using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Commands.Authentication;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Dtos.Authentication;
using WorxForge.Domain.Dtos.Profile;
using WorxForge.Domain.Queries.Profile;
using WorxForge.Infrastructure.Options;

namespace WorxForge.API.UnitTests.Configuration;

public sealed class GuestAccessTests
{
    private static async Task<WebApplication> StartAsync(bool guestAccess)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["GuestAccess:Enabled"] = guestAccess.ToString(),
            ["JwtAccessToken:Issuer"] = "test-issuer",
            ["JwtAccessToken:Audience"] = "test-audience",
            ["JwtAccessToken:SigningKey"] = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
            ["JwtAccessToken:ExpirationMinutes"] = "60"
        });
        builder.Services.Configure<GuestAccessOptions>(builder.Configuration.GetSection("GuestAccess"));
        builder.Services.Configure<JwtAccessTokenOptions>(builder.Configuration.GetSection("JwtAccessToken"));
        builder.Services.AddWorxForgeAuthentication(builder.Configuration);
        builder.Services.AddWorxForgeAuthorization();
        var profiles = new Mock<IQueryHandler<GetMyProfileQuery, ProfileDto>>();
        profiles.Setup(handler => handler.HandleAsync(It.IsAny<GetMyProfileQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetMyProfileQuery query, CancellationToken _) => new ProfileDto
            {
                UserId = query.UserId,
                Username = GuestWorkspaceConstants.Username,
                DisplayName = GuestWorkspaceConstants.DisplayName
            });
        builder.Services.AddSingleton(profiles.Object);
        builder.Services.AddSingleton(Mock.Of<ICommandHandler<LogoutUserCommand>>());
        builder.Services.AddSingleton(Mock.Of<ICommandHandler<LoginUserCommand, AuthTokenDto>>());
        builder.Services.AddSingleton(Mock.Of<IAuthenticationService>());
        var app = builder.Build();
        app.Urls.Add("http://127.0.0.1:0");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapAuthenticationEndpoints();
        app.MapSettingsEndpoints();
        app.MapGet("/identity", (ClaimsPrincipal user) => Results.Ok(new
        {
            UserId = user.FindFirstValue(ClaimConstants.UserId),
            NameIdentifier = user.FindFirstValue(ClaimTypes.NameIdentifier)
        })).RequireAuthorization(PolicyConstants.AuthenticatedUser);
        app.MapGet("/admin-only", () => Results.Ok()).RequireAuthorization(PolicyConstants.AdminOnly);
        await app.StartAsync();
        return app;
    }

    private static HttpClient Client(WebApplication app)
    {
        var address = app.Services.GetRequiredService<IServer>().Features
            .Get<IServerAddressesFeature>()!.Addresses.Single();
        return new HttpClient { BaseAddress = new Uri(address) };
    }

    [Fact]
    public async Task GuestMode_AllowsTokenlessRequests_AndIgnoresIncomingBearerIdentity()
    {
        await using var app = await StartAsync(true);
        using var client = Client(app);
        client.DefaultRequestHeaders.Authorization = new("Bearer", "untrusted-token");
        var identity = await client.GetFromJsonAsync<Dictionary<string, string>>("/identity");
        Assert.Equal(GuestWorkspaceConstants.UserId.ToString(), identity!["userId"]);
        Assert.Equal(identity["userId"], identity["nameIdentifier"]);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/authentication/me")).StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, (await client.GetAsync("/admin-only")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.PostAsJsonAsync("/api/authentication/login", new { idToken = "unused" })).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.PostAsJsonAsync("/api/authentication/debug-login", new { })).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await client.PostAsync("/api/authentication/logout", null)).StatusCode);
    }

    [Fact]
    public async Task DisabledGuestMode_RequiresAuthentication()
    {
        await using var app = await StartAsync(false);
        using var client = Client(app);
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/identity")).StatusCode);
        var mode = await client.GetFromJsonAsync<Dictionary<string, bool>>("/api/authentication/access-mode");
        Assert.False(mode!["guestAccessEnabled"]);
    }

    [Fact]
    public async Task EmailSettings_ReturnSkipped_InsteadOfThrowingOrClaimingVerification()
    {
        await using var app = await StartAsync(true);
        using var client = Client(app);
        var update = await client.PutAsJsonAsync("/api/settings/email", new
        {
            userId = GuestWorkspaceConstants.UserId,
            newEmail = "user@example.com"
        });
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.Equal("skipped", (await update.Content.ReadFromJsonAsync<Dictionary<string, string>>())!["status"]);
        var verify = await client.PostAsJsonAsync("/api/settings/verify-email", new
        {
            userId = GuestWorkspaceConstants.UserId,
            token = "unused"
        });
        Assert.Equal(HttpStatusCode.OK, verify.StatusCode);
        Assert.Equal("skipped", (await verify.Content.ReadFromJsonAsync<Dictionary<string, string>>())!["status"]);
    }
}
