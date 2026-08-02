using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Authentication;

namespace WorxForge.Core.Validators.Authentication;

public sealed class GetMyProfileQueryValidator : IRequestValidator<GetMyProfileQuery>
{
    public Task ValidateAsync(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
