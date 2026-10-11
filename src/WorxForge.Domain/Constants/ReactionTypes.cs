namespace WorxForge.Domain.Constants;

public static class ReactionTypes
{
    // Keep the stored/wire value so existing positive feedback remains valid.
    public const string Like = "Like";

    public static bool IsSupported(string? reactionType) =>
        string.Equals(reactionType, Like, StringComparison.OrdinalIgnoreCase);

    public static string Normalize(string reactionType)
    {
        if (IsSupported(reactionType))
        {
            return Like;
        }

        throw new ArgumentOutOfRangeException(nameof(reactionType), reactionType, "Only Useful (Like) feedback is supported.");
    }
}
