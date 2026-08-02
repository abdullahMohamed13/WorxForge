namespace WorxForge.Domain.Commands.Library;

public sealed class UnfollowAssetCommand
{
    public Guid UserId { get; init; }
    public Guid AssetId { get; init; }
}

