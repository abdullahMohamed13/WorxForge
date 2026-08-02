using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Explore;

namespace WorxForge.Core.Handlers.Explore;

public sealed class BrowseAssetsQueryHandler : IQueryHandler<BrowseAssetsQuery, WorxForge.Domain.Dtos.Assets.AssetSummaryDto>
{
    public Task<WorxForge.Domain.Dtos.Assets.AssetSummaryDto> HandleAsync(BrowseAssetsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
