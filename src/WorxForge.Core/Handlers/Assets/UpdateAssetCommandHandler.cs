using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Assets;

namespace WorxForge.Core.Handlers.Assets;

public sealed class UpdateAssetCommandHandler : ICommandHandler<UpdateAssetCommand>
{
    public Task HandleAsync(UpdateAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
