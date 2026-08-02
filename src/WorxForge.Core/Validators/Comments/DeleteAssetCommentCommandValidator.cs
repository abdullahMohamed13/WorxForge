using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Comments;

namespace WorxForge.Core.Validators.Comments;

public sealed class DeleteAssetCommentCommandValidator : IRequestValidator<DeleteAssetCommentCommand>
{
    public Task ValidateAsync(DeleteAssetCommentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
