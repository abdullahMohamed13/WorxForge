using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Explore;

namespace WorxForge.Core.Handlers.Explore;

public sealed class SearchAssetsQueryHandler : IQueryHandler<SearchAssetsQuery, WorxForge.Domain.Dtos.Assets.AssetSummaryDto>
{
    public Task<WorxForge.Domain.Dtos.Assets.AssetSummaryDto> HandleAsync(SearchAssetsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
