using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Comments;

namespace WorxForge.Core.Validators.Comments;

public sealed class CreateAssetCommentCommandValidator : IRequestValidator<CreateAssetCommentCommand>
{
    public Task ValidateAsync(CreateAssetCommentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
