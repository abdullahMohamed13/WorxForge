using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class TagRepository : ITagRepository
{
    public Task<IReadOnlyList<Tag>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Tag> GetOrCreateAsync(string name, CancellationToken cancellationToken) => throw new NotImplementedException();
}
