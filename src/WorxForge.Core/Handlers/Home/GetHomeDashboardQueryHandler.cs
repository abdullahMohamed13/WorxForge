using WorxForge.Core.Abstractions;
using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Home;
using WorxForge.Domain.Queries.Home;

namespace WorxForge.Core.Handlers.Home;

public sealed class GetHomeDashboardQueryHandler(IBusinessDomainRepository businessDomainRepository, IAssetRepository assetRepository)
    : IQueryHandler<GetHomeDashboardQuery, HomeDashboardDto>
{
    private const int UsefulThisWeekLimit = 4;
    private const int RecentlyUpdatedLimit = 4;
    private const int RecommendedForYouLimit = 4;
    private const int PopularAcrossTeamsLimit = 4;

    public async Task<HomeDashboardDto> HandleAsync(GetHomeDashboardQuery query, CancellationToken cancellationToken)
    {
        var domainSummaries = await businessDomainRepository.GetDomainSummariesAsync(cancellationToken);
        var usefulThisWeek = await assetRepository.GetUsefulThisWeekAsync(query.UserId, UsefulThisWeekLimit, cancellationToken);
        var recentlyUpdated = await assetRepository.GetRecentlyUpdatedAsync(query.UserId, RecentlyUpdatedLimit, cancellationToken);
        var recommendedForYou = await assetRepository.GetRecommendedForYouAsync(query.UserId, RecommendedForYouLimit, cancellationToken);
        var popularAcrossTeams = await assetRepository.GetPopularAcrossTeamsAsync(query.UserId, PopularAcrossTeamsLimit, cancellationToken);

        return new HomeDashboardDto
        {
            DomainSummaries = domainSummaries,
            UsefulThisWeek = usefulThisWeek,
            RecentlyUpdated = recentlyUpdated,
            RecommendedForYou = recommendedForYou,
            PopularAcrossTeams = popularAcrossTeams
        };
    }
}
