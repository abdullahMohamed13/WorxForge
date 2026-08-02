using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Notifications;

namespace WorxForge.Core.Validators.Notifications;

public sealed class MarkNotificationAsReadCommandValidator : IRequestValidator<MarkNotificationAsReadCommand>
{
    public Task ValidateAsync(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
