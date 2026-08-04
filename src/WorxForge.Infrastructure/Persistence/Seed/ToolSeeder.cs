using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class ToolSeeder : IDataSeeder
{
    private static readonly string[] Names =
    [
        "WorxGPT",
        "Claude Code",
        "GitHub Copilot",
        "OpenAI",
        "Anthropic",
        "Tool agnostic"
    ];

    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Tools.AnyAsync(cancellationToken))
        {
            return;
        }

        var tools = Names.Select(name => new Tool { ToolId = Guid.NewGuid(), Name = name });
        await context.Tools.AddRangeAsync(tools, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
