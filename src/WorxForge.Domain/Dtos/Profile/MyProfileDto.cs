namespace WorxForge.Domain.Dtos.Profile;

public sealed class MyProfileDto
{
    public Guid UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool EmailVerified { get; init; }
    public string? AvatarUrl { get; init; }
    public DateTime CreatedAt { get; init; }
}

