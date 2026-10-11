namespace WorxForge.Domain.Enums;

public enum NotificationType
{
    CommentOnAsset = 0,
    CommentReply = 1,
    PostLiked = 2,
    // Retired: preserve the value to interpret/filter historical notifications.
    PostDisliked = 3,
    Announcement = 4,
    // Fallback for stored values that no longer match a known member.
    Unknown = -1
}
