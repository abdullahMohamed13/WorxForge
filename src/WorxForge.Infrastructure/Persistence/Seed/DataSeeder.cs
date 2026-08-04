using Microsoft.Extensions.DependencyInjection;
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

        foreach (var seeder in Seeders)
        {
            await seeder.SeedAsync(context, cancellationToken);
        }
    }
}
