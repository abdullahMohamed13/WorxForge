using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class BookmarkRepository : IBookmarkRepository
{
    public Task<bool> ExistsAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetSummaryDto> Items, int Total)> GetByUserAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(Bookmark bookmark, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task DeleteAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
