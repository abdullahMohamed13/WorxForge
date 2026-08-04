using Microsoft.EntityFrameworkCore;
using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Entities;
using WorxForge.Domain.Queries.Explore;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Repositories;

public sealed class AssetRepository(WorxForgeDbContext dbContext) : IAssetRepository
{
    public Task<Asset?> GetByIdAsync(Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetSummaryDto> Items, int Total)> SearchAsync(SearchAssetsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetSummaryDto> Items, int Total)> BrowseAsync(BrowseAssetsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetSummaryDto> Items, int Total)> GetByOwnerAsync(Guid userId, int page, int pageSize, string? sortBy, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IReadOnlyList<AssetSummaryDto>> GetFeaturedAsync(int limit, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IReadOnlyList<AssetSummaryDto>> GetTrendingAsync(int limit, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<IReadOnlyList<AssetSummaryDto>> GetUsefulThisWeekAsync(Guid userId, int limit, CancellationToken cancellationToken)
    {
        var since = DateTime.UtcNow.AddDays(-7);

        return await dbContext.Assets
            .AsNoTracking()
            .Where(asset => asset.CreatedAt >= since)
            .OrderByDescending(asset => dbContext.Reactions.Count(reaction =>
                reaction.AssetId == asset.AssetId && reaction.ReactionType == ReactionTypeConstants.Like))
            .Take(limit)
            .Select(asset => new AssetSummaryDto
            {
                AssetId = asset.AssetId,
                Title = asset.Title,
                Description = asset.Description,
                AssetTypeName = dbContext.AssetTypes
                    .Where(assetType => assetType.AssetTypeId == asset.AssetTypeId)
                    .Select(assetType => assetType.Name)
                    .First(),
                DomainName = dbContext.BusinessDomains
                    .Where(domain => domain.DomainId == asset.DomainId)
                    .Select(domain => domain.Name)
                    .First(),
                Tools = dbContext.AssetTools
                    .Where(assetTool => assetTool.AssetId == asset.AssetId)
                    .Join(dbContext.Tools, assetTool => assetTool.ToolId, tool => tool.ToolId, (assetTool, tool) => tool.Name)
                    .ToList(),
                AuthorDisplayName = dbContext.Users
                    .Where(user => user.UserId == asset.AuthorId)
                    .Select(user => user.DisplayName)
                    .First(),
                CreatedAt = asset.CreatedAt,
                UpdatedAt = asset.UpdatedAt,
                ReactionCount = dbContext.Reactions.Count(reaction => reaction.AssetId == asset.AssetId),
                CommentCount = dbContext.Comments.Count(comment => comment.AssetId == asset.AssetId && comment.DeletedAt == null),
                IsBookmarked = dbContext.Bookmarks.Any(bookmark => bookmark.AssetId == asset.AssetId && bookmark.UserId == userId)
            })
            .ToListAsync(cancellationToken);
    }

    public Task AddAsync(Asset asset, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateAsync(Asset asset, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task DeleteAsync(Guid assetId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
