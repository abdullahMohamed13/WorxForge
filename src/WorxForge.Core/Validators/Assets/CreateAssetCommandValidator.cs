using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Assets;

namespace WorxForge.Core.Validators.Assets;

public sealed class CreateAssetCommandValidator : IRequestValidator<CreateAssetCommand>
{
    public Task ValidateAsync(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
