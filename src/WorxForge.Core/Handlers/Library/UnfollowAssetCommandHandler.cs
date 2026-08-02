using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Handlers.Library;

public sealed class UnfollowAssetCommandHandler : ICommandHandler<UnfollowAssetCommand>
{
    public Task HandleAsync(UnfollowAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
