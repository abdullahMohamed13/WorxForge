using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IReadOnlyList<User>> GetAllAsync(string? search, string? sortBy, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<int> CountAsync(string? search, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(User user, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateAsync(User user, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task SoftDeleteAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
