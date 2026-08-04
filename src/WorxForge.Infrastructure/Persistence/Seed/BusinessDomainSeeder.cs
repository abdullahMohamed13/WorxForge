using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class BusinessDomainSeeder : IDataSeeder
{
    private static readonly string[] Names =
    [
        "Development",
        "Quality Assurance",
        "Product",
        "Architecture",
        "DevOps",
        "Security",
        "Data",
        "General"
    ];

    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.BusinessDomains.AnyAsync(cancellationToken))
        {
            return;
        }

        var domains = Names.Select(name => new BusinessDomain { DomainId = Guid.NewGuid(), Name = name });
        await context.BusinessDomains.AddRangeAsync(domains, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
