using WorxForge.API.EndpointGroups;
using WorxForge.API.Middlewares;

namespace WorxForge.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseWorxForgePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static WebApplication MapWorxForgeEndpoints(this WebApplication app)
    {
        app.MapAuthenticationEndpoints();
        app.MapProfileEndpoints();
        app.MapSettingsEndpoints();
        app.MapHomeEndpoints();
        app.MapExploreEndpoints();
        app.MapAssetsEndpoints();
        app.MapLibraryEndpoints();
        app.MapNotificationsEndpoints();
        app.MapAdminEndpoints();
        return app;
    }
}
