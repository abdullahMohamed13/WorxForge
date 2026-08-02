using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Assets;

namespace WorxForge.Core.Validators.Assets;

public sealed class GetAssetDetailsQueryValidator : IRequestValidator<GetAssetDetailsQuery>
{
    public Task ValidateAsync(GetAssetDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
