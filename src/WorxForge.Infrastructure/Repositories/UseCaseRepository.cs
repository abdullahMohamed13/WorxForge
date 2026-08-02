using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class UseCaseRepository : IUseCaseRepository
{
    public Task<IReadOnlyList<UseCase>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<UseCase?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}
