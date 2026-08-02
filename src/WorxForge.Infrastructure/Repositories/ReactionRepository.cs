using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class ReactionRepository : IReactionRepository
{
    public Task<Reaction?> GetAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<int> CountByAssetAsync(Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpsertAsync(Reaction reaction, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task DeleteAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
