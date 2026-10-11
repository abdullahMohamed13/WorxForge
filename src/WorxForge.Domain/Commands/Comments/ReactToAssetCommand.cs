namespace WorxForge.Domain.Commands.Comments;

// Useful feedback retains the Like value for existing clients and stored rows.
public sealed class ReactToAssetCommand
{
    public Guid UserId { get; init; }
    public Guid AssetId { get; init; }
    public string ReactionType { get; init; } = string.Empty;
}

