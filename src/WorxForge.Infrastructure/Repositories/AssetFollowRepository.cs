using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Assets;

namespace WorxForge.Infrastructure.Repositories;

public sealed class AssetFollowRepository : IAssetFollowRepository
{
    public Task<bool> ExistsAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetSummaryDto> Items, int Total)> GetFollowedByUserAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task DeleteAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
