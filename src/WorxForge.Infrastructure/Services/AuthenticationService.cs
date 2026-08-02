using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Dtos.Authentication;

namespace WorxForge.Infrastructure.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    public Task RegisterAsync(string email, string username, string password, string passwordConfirm, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<AuthTokenDto> AuthenticateAsync(string emailOrUsername, string password, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task VerifyEmailAsync(string token, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task RequestPasswordResetAsync(string email, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task ResetPasswordAsync(string token, string newPassword, string newPasswordConfirm, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task RevokeSessionAsync(Guid userId, Guid sessionId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
