using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class AssetTypeRepository : IAssetTypeRepository
{
    public Task<IReadOnlyList<AssetType>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<AssetType?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}
