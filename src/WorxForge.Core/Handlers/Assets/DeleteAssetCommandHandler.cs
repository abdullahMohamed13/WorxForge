using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Assets;

namespace WorxForge.Core.Handlers.Assets;

public sealed class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand>
{
    public Task HandleAsync(DeleteAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
