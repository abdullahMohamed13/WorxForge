using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Metadata;
using WorxForge.API.Models.Responses.Common;

namespace WorxForge.API.Middlewares;

// Apply endpoint body limits before minimal API form binding reads or buffers uploads.
public sealed class RequestBodyLimitMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<IRequestSizeLimitMetadata>()?.MaxRequestBodySize is long limit)
        {
            if (context.Request.ContentLength > limit)
            {
                context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
                await context.Response.WriteAsJsonAsync(
                    new ApiErrorResponse("Upload.TooLarge", "The upload request exceeds the size limit."), context.RequestAborted);
                return;
            }

            // Also bound chunked requests, which have no Content-Length header.
            var feature = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
            if (feature is { IsReadOnly: false })
                feature.MaxRequestBodySize = Math.Min(feature.MaxRequestBodySize ?? limit, limit);
        }
        await next(context);
    }
}
