using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Validators.Library;

public sealed class UnfollowAssetCommandValidator : IRequestValidator<UnfollowAssetCommand>
{
    public Task ValidateAsync(UnfollowAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
