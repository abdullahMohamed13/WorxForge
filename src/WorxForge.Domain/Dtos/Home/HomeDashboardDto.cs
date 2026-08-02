using WorxForge.Domain.Dtos.Assets;

namespace WorxForge.Domain.Dtos.Home;

public sealed class HomeDashboardDto
{
    // TODO: Confirm recommendation algorithm design.
    public IReadOnlyList<AssetSummaryDto> Recommendations { get; init; } = [];
    public IReadOnlyList<AssetSummaryDto> Featured { get; init; } = [];
    public IReadOnlyList<AssetSummaryDto> Trending { get; init; } = [];
    public IReadOnlyList<AssetSummaryDto> RecentlyAccessed { get; init; } = [];
    public IReadOnlyList<string> Announcements { get; init; } = [];
}

