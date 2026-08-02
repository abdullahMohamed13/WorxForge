namespace WorxForge.Domain.Commands.Authentication;

public sealed class LoginUserCommand
{
    // EmailOrUsername accepted per SRS FR-005
    public string EmailOrUsername { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

