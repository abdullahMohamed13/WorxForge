using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WorxForge.API.Configuration;

public static class AuthenticationConfigurationExtensions
{
    public static IServiceCollection AddJwtAccessTokenAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}
