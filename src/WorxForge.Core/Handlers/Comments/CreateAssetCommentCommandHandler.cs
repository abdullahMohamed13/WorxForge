using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Comments;

namespace WorxForge.Core.Handlers.Comments;

public sealed class CreateAssetCommentCommandHandler : ICommandHandler<CreateAssetCommentCommand>
{
    public Task HandleAsync(CreateAssetCommentCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
