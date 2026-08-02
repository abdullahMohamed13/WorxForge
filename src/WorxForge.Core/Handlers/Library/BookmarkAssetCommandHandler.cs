using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Handlers.Library;

public sealed class BookmarkAssetCommandHandler : ICommandHandler<BookmarkAssetCommand>
{
    public Task HandleAsync(BookmarkAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
