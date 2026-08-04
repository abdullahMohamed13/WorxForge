using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class AssetTypeSeeder : IDataSeeder
{
    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.AssetTypes.AnyAsync(cancellationToken))
        {
            return;
        }

        var assetTypes = AssetTypeConstants.All.Select(entry => new AssetType { AssetTypeId = entry.Id, Name = entry.Name });
        await context.AssetTypes.AddRangeAsync(assetTypes, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
