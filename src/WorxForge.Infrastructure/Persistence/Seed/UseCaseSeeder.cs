using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class UseCaseSeeder : IDataSeeder
{
    public async Task SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.UseCases.AnyAsync(cancellationToken))
        {
            return;
        }

        var useCases = UseCaseConstants.All.Select(entry => new UseCase { UseCaseId = entry.Id, Name = entry.Name });
        await context.UseCases.AddRangeAsync(useCases, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
