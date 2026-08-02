using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Assets;

namespace WorxForge.Core.Handlers.Assets;

public sealed class GetAssetCommentsQueryHandler : IQueryHandler<GetAssetCommentsQuery, WorxForge.Domain.Dtos.Assets.AssetCommentDto>
{
    public Task<WorxForge.Domain.Dtos.Assets.AssetCommentDto> HandleAsync(GetAssetCommentsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
