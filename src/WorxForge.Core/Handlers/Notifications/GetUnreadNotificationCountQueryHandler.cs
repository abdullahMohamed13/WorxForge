using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Notifications;

namespace WorxForge.Core.Handlers.Notifications;

public sealed class GetUnreadNotificationCountQueryHandler : IQueryHandler<GetUnreadNotificationCountQuery, WorxForge.Domain.Dtos.Notifications.UnreadNotificationCountDto>
{
    public Task<WorxForge.Domain.Dtos.Notifications.UnreadNotificationCountDto> HandleAsync(GetUnreadNotificationCountQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
