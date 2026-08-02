using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Validators.Library;

public sealed class FollowAssetCommandValidator : IRequestValidator<FollowAssetCommand>
{
    public Task ValidateAsync(FollowAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
