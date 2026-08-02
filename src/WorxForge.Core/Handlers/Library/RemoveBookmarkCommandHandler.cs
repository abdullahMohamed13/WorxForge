using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Handlers.Library;

public sealed class RemoveBookmarkCommandHandler : ICommandHandler<RemoveBookmarkCommand>
{
    public Task HandleAsync(RemoveBookmarkCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
