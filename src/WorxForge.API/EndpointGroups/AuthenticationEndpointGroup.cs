using WorxForge.Domain.Commands.Authentication;

namespace WorxForge.API.EndpointGroups;

public static class AuthenticationEndpointGroup
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/authentication");
        group.MapPost("/register", RegisterUserAsync);
        group.MapPost("/verify-email", VerifyEmailAsync);
        group.MapPost("/login", LoginUserAsync);
        group.MapPost("/logout", LogoutUserAsync);
        group.MapPost("/request-password-reset", RequestPasswordResetAsync);
        group.MapPost("/reset-password", ResetPasswordAsync);
        return group;
    }

    private static Task<IResult> RegisterUserAsync(RegisterUserCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> VerifyEmailAsync(VerifyEmailCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> LogoutUserAsync(LogoutUserCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> RequestPasswordResetAsync(RequestPasswordResetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> ResetPasswordAsync(ResetPasswordCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}
