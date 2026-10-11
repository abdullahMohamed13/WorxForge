namespace WorxForge.Infrastructure.Options;

public sealed class GuestAccessOptions
{
    public const string SectionName = "GuestAccess";
    // TODO: Login is skipped pending adding an SMTP account and completing account flows.
    public bool Enabled { get; set; } = true;
}
