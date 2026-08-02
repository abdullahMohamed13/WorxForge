using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Notifications;

namespace WorxForge.Core.Handlers.Notifications;

public sealed class GetNotificationsQueryHandler : IQueryHandler<GetNotificationsQuery, WorxForge.Domain.Dtos.Notifications.NotificationDto>
{
    public Task<WorxForge.Domain.Dtos.Notifications.NotificationDto> HandleAsync(GetNotificationsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
