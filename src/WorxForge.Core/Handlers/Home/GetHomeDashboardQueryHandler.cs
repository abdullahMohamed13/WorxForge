using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Home;

namespace WorxForge.Core.Handlers.Home;

public sealed class GetHomeDashboardQueryHandler : IQueryHandler<GetHomeDashboardQuery, WorxForge.Domain.Dtos.Home.HomeDashboardDto>
{
    public Task<WorxForge.Domain.Dtos.Home.HomeDashboardDto> HandleAsync(GetHomeDashboardQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
