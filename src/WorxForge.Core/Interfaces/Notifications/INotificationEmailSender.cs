namespace WorxForge.Core.Interfaces.Notifications;

public interface INotificationEmailSender
{
    Task SendAsync(Guid recipientUserId, string message, CancellationToken cancellationToken);
}
