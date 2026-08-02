using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Comments;

namespace WorxForge.Core.Handlers.Comments;

public sealed class DeleteAssetCommentCommandHandler : ICommandHandler<DeleteAssetCommentCommand>
{
    public Task HandleAsync(DeleteAssetCommentCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
