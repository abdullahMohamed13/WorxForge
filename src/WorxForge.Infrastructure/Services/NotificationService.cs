using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Dtos.Common;
using WorxForge.Domain.Dtos.Notifications;
using WorxForge.Domain.Enums;

namespace WorxForge.Infrastructure.Services;

public sealed class NotificationService : INotificationService
{
    public Task<PagedResultDto<NotificationDto>> GetNotificationsAsync(Guid userId, bool? isRead, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task MarkAsReadAsync(Guid userId, Guid notificationId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task SendAsync(Guid recipientUserId, Guid? actorUserId, NotificationType type, string message, Guid? assetId, Guid? commentId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
