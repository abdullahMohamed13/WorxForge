using WorxForge.Core.Interfaces.Services;

namespace WorxForge.Infrastructure.Services;

public sealed class SettingsService : ISettingsService
{
    public Task UpdateEmailAddressAsync(Guid userId, string newEmail, CancellationToken cancellationToken)
    {
        // TODO: Email changes/verification are skipped pending adding an SMTP account.
        // Do not mutate or mark an unverified email as verified while delivery is disabled.
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
    public Task VerifyUpdatedEmailAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        // TODO: Email changes/verification are skipped pending adding an SMTP account.
        // Do not mutate or mark an unverified email as verified while delivery is disabled.
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
    public Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string newPasswordConfirm, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task DeleteAccountAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
