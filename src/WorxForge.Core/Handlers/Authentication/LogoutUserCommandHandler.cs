using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Authentication;

namespace WorxForge.Core.Handlers.Authentication;

public sealed class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
{
    public Task HandleAsync(LogoutUserCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
