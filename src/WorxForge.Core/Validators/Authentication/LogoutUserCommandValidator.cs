using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Authentication;

namespace WorxForge.Core.Validators.Authentication;

public sealed class LogoutUserCommandValidator : IRequestValidator<LogoutUserCommand>
{
    public Task ValidateAsync(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
