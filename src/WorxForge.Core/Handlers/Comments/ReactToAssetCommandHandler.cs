using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Comments;

namespace WorxForge.Core.Handlers.Comments;

public sealed class ReactToAssetCommandHandler : ICommandHandler<ReactToAssetCommand>
{
    public Task HandleAsync(ReactToAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
