using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Library;

namespace WorxForge.Core.Handlers.Library;

public sealed class FollowAssetCommandHandler : ICommandHandler<FollowAssetCommand>
{
    public Task HandleAsync(FollowAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
