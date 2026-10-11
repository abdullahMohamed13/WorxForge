using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using WorxForge.Infrastructure.Options;
using WorxForge.Domain.Constants;
using WorxForge.Core.Abstractions;
using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Commands.Authentication;
using WorxForge.Domain.Dtos.Authentication;
using WorxForge.Domain.Dtos.Profile;
using WorxForge.Domain.Errors;
using WorxForge.Domain.Exceptions;
using WorxForge.Domain.Queries.Profile;

namespace WorxForge.API.EndpointGroups;

public static class AuthenticationEndpointGroup
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/authentication");
        var guestAccess = app.ServiceProvider.GetRequiredService<IOptions<GuestAccessOptions>>().Value.Enabled;
        group.MapGet("/access-mode", () => Results.Ok(new { GuestAccessEnabled = guestAccess })).AllowAnonymous();
        // TODO: Login is skipped pending adding an SMTP account and completing account flows.
        if (!guestAccess)
        {
            group.MapPost("/login", LoginUserAsync);
            if (app is WebApplication webApplication && webApplication.Environment.IsDevelopment())
            {
                group.MapPost("/debug-login", DebugLoginAsync);
                group.MapPost("/debug-login-helper", DebugLoginHelperAsync);
            }
        }
        group.MapPost("/logout", LogoutUserAsync).RequireAuthorization(PolicyConstants.AuthenticatedUser);
        group.MapGet("/me", GetMyProfileAsync).RequireAuthorization(PolicyConstants.AuthenticatedUser);
        return group;
    }

    private static async Task<IResult> LoginUserAsync(
        LoginUserCommand command,
        ICommandHandler<LoginUserCommand, AuthTokenDto> handler,
        CancellationToken cancellationToken)
    {
        var token = await handler.HandleAsync(command, cancellationToken);
        return Results.Ok(token);
    }

    private static async Task<IResult> DebugLoginAsync(
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var token = await authenticationService.AuthenticateDevelopmentUserAsync(cancellationToken);
        return Results.Ok(token);
    }

    private static async Task<IResult> DebugLoginHelperAsync(
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var token = await authenticationService.AuthenticateDevelopmentUserAsync(cancellationToken);

        return Results.Ok(new
        {
            token.AccessToken,
            token.ExpiresAt,
            SwaggerAuthorizeValue = token.AccessToken,
            AuthorizationHeaderValue = $"Bearer {token.AccessToken}"
        });
    }

    private static async Task<IResult> LogoutUserAsync(
        HttpContext httpContext,
        ICommandHandler<LogoutUserCommand> handler,
        IOptions<GuestAccessOptions> guestAccess,
        CancellationToken cancellationToken)
    {
        if (guestAccess.Value.Enabled)
        {
            return Results.NoContent();
        }

        var userIdClaim = httpContext.User.FindFirst(ClaimConstants.UserId)?.Value;
        var sessionIdClaim = httpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId) || !Guid.TryParse(sessionIdClaim, out var sessionId))
        {
            throw new UnauthorizedException(Errors.AuthenticationInvalidCredentials);
        }

        await handler.HandleAsync(
            new LogoutUserCommand
            {
                UserId = userId,
                SessionId = sessionId
            },
            cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> GetMyProfileAsync(
        HttpContext httpContext,
        IQueryHandler<GetMyProfileQuery, ProfileDto> handler,
        CancellationToken cancellationToken)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimConstants.UserId)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedException(Errors.AuthenticationInvalidCredentials);
        }

        var profile = await handler.HandleAsync(new GetMyProfileQuery { UserId = userId }, cancellationToken);
        return Results.Ok(new
        {
            profile.UserId,
            profile.Username,
            profile.DisplayName,
            profile.AvatarUrl,
            profile.CreatedAt,
            Role = httpContext.User.FindFirst(ClaimConstants.Role)?.Value ?? "User"
        });
    }
}
