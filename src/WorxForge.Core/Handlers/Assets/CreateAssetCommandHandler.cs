using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Assets;

namespace WorxForge.Core.Handlers.Assets;

public sealed class CreateAssetCommandHandler : ICommandHandler<CreateAssetCommand>
{
    public Task HandleAsync(CreateAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
