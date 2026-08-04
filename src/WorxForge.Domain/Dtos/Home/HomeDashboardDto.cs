using WorxForge.Domain.Dtos.Assets;

namespace WorxForge.Domain.Dtos.Home;

public sealed class HomeDashboardDto
{
    // TODO: Confirm recommendation algorithm design.
    public IReadOnlyList<AssetSummaryDto> UsefulThisWeek { get; init; } = [];
    public IReadOnlyList<AssetSummaryDto> RecentlyUpdated { get; init; } = [];
    public IReadOnlyList<AssetSummaryDto> RecommendedForYou { get; init; } = [];
    public IReadOnlyList<AssetSummaryDto> PopularAcrossTeams { get; init; } = [];
    public IReadOnlyList<DomainSummaryDto> DomainSummaries { get; init; } = [];
}

