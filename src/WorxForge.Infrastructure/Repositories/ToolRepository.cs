using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class ToolRepository : IToolRepository
{
    public Task<IReadOnlyList<Tool>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Tool?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}
