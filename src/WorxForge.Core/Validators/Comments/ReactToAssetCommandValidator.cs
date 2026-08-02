using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Comments;

namespace WorxForge.Core.Validators.Comments;

public sealed class ReactToAssetCommandValidator : IRequestValidator<ReactToAssetCommand>
{
    public Task ValidateAsync(ReactToAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
