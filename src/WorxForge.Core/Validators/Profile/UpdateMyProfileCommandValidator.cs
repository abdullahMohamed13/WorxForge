using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Profile;

namespace WorxForge.Core.Validators.Profile;

public sealed class UpdateMyProfileCommandValidator : IRequestValidator<UpdateMyProfileCommand>
{
    public Task ValidateAsync(UpdateMyProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
