using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Notifications;

namespace WorxForge.Core.Validators.Notifications;

public sealed class GetNotificationsQueryValidator : IRequestValidator<GetNotificationsQuery>
{
    public Task ValidateAsync(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
