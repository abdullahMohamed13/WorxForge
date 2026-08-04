using WorxForge.Core.Abstractions;
using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Home;
using WorxForge.Domain.Queries.Home;

namespace WorxForge.Core.Handlers.Home;

public sealed class GetHomeDashboardQueryHandler(IBusinessDomainRepository businessDomainRepository)
    : IQueryHandler<GetHomeDashboardQuery, HomeDashboardDto>
{
    public async Task<HomeDashboardDto> HandleAsync(GetHomeDashboardQuery query, CancellationToken cancellationToken)
    {
        var domainSummaries = await businessDomainRepository.GetDomainSummariesAsync(cancellationToken);

        return new HomeDashboardDto
        {
            DomainSummaries = domainSummaries
        };
    }
}
