using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class BusinessDomainSeeder : IDataSeeder
{
    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.BusinessDomains.AnyAsync(cancellationToken))
        {
            return;
        }

        var domains = BusinessDomainConstants.All.Select(entry => new BusinessDomain { DomainId = entry.Id, Name = entry.Name });
        await context.BusinessDomains.AddRangeAsync(domains, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
