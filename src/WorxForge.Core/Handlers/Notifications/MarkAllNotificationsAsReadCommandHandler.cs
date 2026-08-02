using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Notifications;

namespace WorxForge.Core.Handlers.Notifications;

public sealed class MarkAllNotificationsAsReadCommandHandler : ICommandHandler<MarkAllNotificationsAsReadCommand>
{
    public Task HandleAsync(MarkAllNotificationsAsReadCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
