using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Validators.Library;

public sealed class RemoveBookmarkCommandValidator : IRequestValidator<RemoveBookmarkCommand>
{
    public Task ValidateAsync(RemoveBookmarkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
