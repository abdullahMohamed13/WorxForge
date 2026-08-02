using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Explore;

namespace WorxForge.Core.Handlers.Explore;

public sealed class GetExploreFiltersQueryHandler : IQueryHandler<GetExploreFiltersQuery, WorxForge.Domain.Dtos.Explore.ExploreFiltersDto>
{
    public Task<WorxForge.Domain.Dtos.Explore.ExploreFiltersDto> HandleAsync(GetExploreFiltersQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
