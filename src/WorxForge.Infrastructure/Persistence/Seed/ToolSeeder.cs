using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class ToolSeeder : IDataSeeder
{
    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Tools.AnyAsync(cancellationToken))
        {
            return;
        }

        var tools = ToolConstants.All.Select(entry => new Tool { ToolId = entry.Id, Name = entry.Name });
        await context.Tools.AddRangeAsync(tools, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
