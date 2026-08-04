using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

/// <summary>Runs all <see cref="IDataSeeder"/> implementations against the database on startup.</summary>
public static class DataSeeder
{
    private static readonly IDataSeeder[] Seeders =
    [
        new AssetTypeSeeder(),
        new BusinessDomainSeeder(),
        new UseCaseSeeder(),
        new ToolSeeder()
    ];

    public static async Task SeedWorxForgeDataAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WorxForgeDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IDataSeeder>>();

        foreach (var seeder in Seeders)
        {
            try
            {
                await seeder.SeedAsync(context, cancellationToken);
            }
            catch (Exception ex)
            {
                // Seeding is best-effort; a failure here must not prevent the app from starting.
                logger.LogError(ex, "{Seeder} failed to seed lookup data", seeder.GetType().Name);
            }
        }
    }
}
