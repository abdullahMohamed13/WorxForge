using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Authentication;

namespace WorxForge.Core.Handlers.Authentication;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    public Task HandleAsync(LoginUserCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
