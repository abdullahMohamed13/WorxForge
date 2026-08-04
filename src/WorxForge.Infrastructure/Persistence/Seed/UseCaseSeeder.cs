using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class UseCaseSeeder : IDataSeeder
{
    private static readonly string[] Names =
    [
        "Code generation",
        "Code review",
        "Testing",
        "Requirements",
        "Documentation",
        "Debugging",
        "Architecture design",
        "Security review",
        "Repository onboarding",
        "General Assistance"
    ];

    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.UseCases.AnyAsync(cancellationToken))
        {
            return;
        }

        var useCases = Names.Select(name => new UseCase { UseCaseId = Guid.NewGuid(), Name = name });
        await context.UseCases.AddRangeAsync(useCases, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
