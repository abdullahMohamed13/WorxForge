using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class BusinessDomainRepository : IBusinessDomainRepository
{
    public Task<IReadOnlyList<BusinessDomain>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<BusinessDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}
