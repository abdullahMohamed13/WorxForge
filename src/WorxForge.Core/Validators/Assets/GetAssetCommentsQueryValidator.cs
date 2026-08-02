using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Assets;

namespace WorxForge.Core.Validators.Assets;

public sealed class GetAssetCommentsQueryValidator : IRequestValidator<GetAssetCommentsQuery>
{
    public Task ValidateAsync(GetAssetCommentsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
