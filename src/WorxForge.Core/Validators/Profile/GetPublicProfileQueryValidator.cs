using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Profile;

namespace WorxForge.Core.Validators.Profile;

public sealed class GetPublicProfileQueryValidator : IRequestValidator<GetPublicProfileQuery>
{
    public Task ValidateAsync(GetPublicProfileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
