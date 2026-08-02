namespace WorxForge.Domain.Entities;

// TODO: Confirm reaction type taxonomy (e.g. Like, Helpful) with product owner.
public sealed class Reaction
{
    public Guid AssetId { get; set; }
    public Guid UserId { get; set; }
    public string ReactionType { get; set; } = string.Empty;
}

