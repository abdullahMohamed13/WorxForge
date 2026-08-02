using WorxForge.Domain.Commands.Notifications;
using WorxForge.Domain.Queries.Notifications;

namespace WorxForge.API.EndpointGroups;

public static class NotificationsEndpointGroup
{
    public static RouteGroupBuilder MapNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notifications");
        group.MapGet("/", GetNotificationsAsync);
        group.MapGet("/unread-count", GetUnreadNotificationCountAsync);
        group.MapPut("/{notificationId:guid}/read", MarkNotificationAsReadAsync);
        group.MapPut("/read-all", MarkAllNotificationsAsReadAsync);
        return group;
    }

    private static Task<IResult> GetNotificationsAsync([AsParameters] GetNotificationsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> GetUnreadNotificationCountAsync([AsParameters] GetUnreadNotificationCountQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> MarkNotificationAsReadAsync(MarkNotificationAsReadCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> MarkAllNotificationsAsReadAsync(MarkAllNotificationsAsReadCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}
