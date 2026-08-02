using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Assets;

namespace WorxForge.Core.Validators.Assets;

public sealed class UpdateAssetCommandValidator : IRequestValidator<UpdateAssetCommand>
{
    public Task ValidateAsync(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
