using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Assets;

namespace WorxForge.Core.Handlers.Assets;

public sealed class GetAssetDetailsQueryHandler : IQueryHandler<GetAssetDetailsQuery, WorxForge.Domain.Dtos.Assets.AssetDetailsDto>
{
    public Task<WorxForge.Domain.Dtos.Assets.AssetDetailsDto> HandleAsync(GetAssetDetailsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
