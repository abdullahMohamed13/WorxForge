using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class AuthSessionRepository : IAuthSessionRepository
{
    public Task<AuthSession?> GetByIdAsync(Guid sessionId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<bool> IsValidAsync(string tokenHash, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(AuthSession session, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task RevokeAsync(Guid sessionId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
