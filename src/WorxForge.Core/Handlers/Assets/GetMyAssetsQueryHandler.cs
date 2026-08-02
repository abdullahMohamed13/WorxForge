using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Assets;

namespace WorxForge.Core.Handlers.Assets;

public sealed class GetMyAssetsQueryHandler : IQueryHandler<GetMyAssetsQuery, WorxForge.Domain.Dtos.Assets.MyAssetDto>
{
    public Task<WorxForge.Domain.Dtos.Assets.MyAssetDto> HandleAsync(GetMyAssetsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
