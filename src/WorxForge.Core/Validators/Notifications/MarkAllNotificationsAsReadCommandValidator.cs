using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Notifications;

namespace WorxForge.Core.Validators.Notifications;

public sealed class MarkAllNotificationsAsReadCommandValidator : IRequestValidator<MarkAllNotificationsAsReadCommand>
{
    public Task ValidateAsync(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
