namespace WorxForge.Domain.Constants;

// Synthetic workspace identity, with no credentials, mailbox, or external account.
// Shared by visitors while login is skipped pending adding an SMTP account.
public static class GuestWorkspaceConstants
{
    public static readonly Guid UserId = new("51111111-1111-1111-1111-111111111101");
    public const string Username = "community-guest";
    public const string DisplayName = "Community Guest";
}
