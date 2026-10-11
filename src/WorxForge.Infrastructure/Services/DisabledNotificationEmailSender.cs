using WorxForge.Core.Interfaces.Notifications;

namespace WorxForge.Infrastructure.Services;

// TODO: Email notifications are skipped pending adding an SMTP account.
// This registered no-op performs no network calls and needs no mailbox or credentials.
public sealed class DisabledNotificationEmailSender : INotificationEmailSender
{
    public Task SendAsync(Guid recipientUserId, string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
}
