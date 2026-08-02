using WorxForge.Domain.Dtos.Assets;

namespace WorxForge.Core.Interfaces.Repositories;

public interface IAssetFollowRepository
{
    Task<bool> ExistsAsync(Guid userId, Guid assetId, CancellationToken cancellationToken);
    Task<(IReadOnlyList<AssetSummaryDto> Items, int Total)> GetFollowedByUserAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken);
    Task AddAsync(Guid userId, Guid assetId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, Guid assetId, CancellationToken cancellationToken);
}

