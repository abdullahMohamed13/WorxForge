using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Notifications;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class NotificationRepository : INotificationRepository
{
    public Task<Notification?> GetByIdAsync(Guid notificationId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<NotificationDto> Items, int Total)> GetByUserAsync(Guid userId, bool? isRead, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(Notification notification, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
