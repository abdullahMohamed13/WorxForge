using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Assets;

namespace WorxForge.Core.Validators.Assets;

public sealed class DeleteAssetCommandValidator : IRequestValidator<DeleteAssetCommand>
{
    public Task ValidateAsync(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
