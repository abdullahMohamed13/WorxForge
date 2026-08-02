namespace WorxForge.Domain.Entities;

// TODO: AUTH_SESSION lifecycle details (single vs multi-session, revocation) pending decision.
public sealed class AuthSession
{
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
}

