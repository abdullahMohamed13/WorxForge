using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Notifications;

namespace WorxForge.Core.Handlers.Notifications;

public sealed class MarkNotificationAsReadCommandHandler : ICommandHandler<MarkNotificationAsReadCommand>
{
    public Task HandleAsync(MarkNotificationAsReadCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
