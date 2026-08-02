using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Validators.Library;

public sealed class BookmarkAssetCommandValidator : IRequestValidator<BookmarkAssetCommand>
{
    public Task ValidateAsync(BookmarkAssetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
