using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Profile;

namespace WorxForge.Core.Handlers.Profile;

public sealed class GetMyProfileStatisticsQueryHandler : IQueryHandler<GetMyProfileStatisticsQuery, WorxForge.Domain.Dtos.Profile.ProfileStatisticsDto>
{
    public Task<WorxForge.Domain.Dtos.Profile.ProfileStatisticsDto> HandleAsync(GetMyProfileStatisticsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
