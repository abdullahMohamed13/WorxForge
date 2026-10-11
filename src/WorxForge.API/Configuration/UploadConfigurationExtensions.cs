using System.Globalization;
using System.Threading.RateLimiting;
using WorxForge.API.Models.Responses.Common;
using WorxForge.Domain.Constants;

namespace WorxForge.API.Configuration;

public static class UploadConfigurationExtensions
{
    public const string PolicyName = "MarkdownUpload";
    // Allow a small multipart envelope in addition to the single 5 MB file.
    public const long MaxRequestBodySizeBytes = AssetValidationConstants.MarkdownFileMaxSizeBytes + 64 * 1024;

    public static IServiceCollection AddWorxForgeUploadProtection(this IServiceCollection services, IConfiguration configuration)
    {
        var permitLimit = configuration.GetValue<int?>("Uploads:PermitLimit") ?? 10;
        var windowSeconds = configuration.GetValue<int?>("Uploads:WindowSeconds") ?? 60;
        if (permitLimit <= 0 || windowSeconds <= 0)
            throw new InvalidOperationException("Uploads:PermitLimit and Uploads:WindowSeconds must be positive.");

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy(PolicyName, context => RateLimitPartition.GetFixedWindowLimiter(
                // Use the connection address, never an untrusted forwarding header or shared guest ID.
                context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = permitLimit,
                    Window = TimeSpan.FromSeconds(windowSeconds),
                    QueueLimit = 0,
                    AutoReplenishment = true
                }));
            options.OnRejected = async (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    context.HttpContext.Response.Headers.RetryAfter = Math.Ceiling(retryAfter.TotalSeconds).ToString(CultureInfo.InvariantCulture);
                await context.HttpContext.Response.WriteAsJsonAsync(
                    new ApiErrorResponse("Upload.RateLimited", "Too many uploads. Please wait and try again."), cancellationToken);
            };
        });
        return services;
    }
}
