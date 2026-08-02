using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Profile;

namespace WorxForge.Core.Handlers.Profile;

public sealed class UpdateMyProfileCommandHandler : ICommandHandler<UpdateMyProfileCommand>
{
    public Task HandleAsync(UpdateMyProfileCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
