using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Notifications;

namespace WorxForge.Core.Validators.Notifications;

public sealed class GetUnreadNotificationCountQueryValidator : IRequestValidator<GetUnreadNotificationCountQuery>
{
    public Task ValidateAsync(GetUnreadNotificationCountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
