using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Commands.Assets;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Dtos.Common;
using WorxForge.Domain.Queries.Explore;

namespace WorxForge.Infrastructure.Services;

public sealed class AssetService : IAssetService
{
    public Task<AssetDetailsDto> GetAssetAsync(Guid assetId, Guid requestingUserId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<AssetDetailsDto> GetMyAssetAsync(Guid assetId, Guid ownerId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<PagedResultDto<MyAssetDto>> GetMyAssetsAsync(Guid userId, int page, int pageSize, string? sortBy, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<PagedResultDto<AssetSummaryDto>> SearchAsync(SearchAssetsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<PagedResultDto<AssetSummaryDto>> BrowseAsync(BrowseAssetsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Guid> CreateAsync(CreateAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateAsync(UpdateAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task DeleteAsync(Guid assetId, Guid requestingUserId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<string> PreviewMarkdownAsync(string markdownContent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
