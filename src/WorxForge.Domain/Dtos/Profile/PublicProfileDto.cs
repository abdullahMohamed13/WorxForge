using WorxForge.Domain.Dtos.Assets;

namespace WorxForge.Domain.Dtos.Profile;

public sealed class PublicProfileDto
{
    public Guid UserId { get; init; }
    public string DisplayName { get; init; } = string.Empty;
    public string? AvatarUrl { get; init; }
    public IReadOnlyList<AssetSummaryDto> PublishedAssets { get; init; } = [];
    public int FollowerCount { get; init; }
}

