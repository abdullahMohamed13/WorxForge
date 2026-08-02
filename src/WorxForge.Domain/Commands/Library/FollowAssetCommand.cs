namespace WorxForge.Domain.Commands.Library;

public sealed class FollowAssetCommand
{
    public Guid UserId { get; init; }
    public Guid AssetId { get; init; }
}

