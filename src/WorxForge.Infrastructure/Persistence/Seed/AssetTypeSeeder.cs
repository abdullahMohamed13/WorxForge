using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class AssetTypeSeeder : IDataSeeder
{
    private static readonly string[] Names =
    [
        "Prompt",
        "Skill",
        "Agent",
        "Agent instruction",
        "Workflow",
        "Coding standard",
        "Template",
        "Other"
    ];

    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.AssetTypes.AnyAsync(cancellationToken))
        {
            return;
        }

        var assetTypes = Names.Select(name => new AssetType { AssetTypeId = Guid.NewGuid(), Name = name });
        await context.AssetTypes.AddRangeAsync(assetTypes, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
