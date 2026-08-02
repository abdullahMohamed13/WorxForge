using Microsoft.EntityFrameworkCore;

namespace WorxForge.Infrastructure.Configuration;

public sealed class WorxForgeDbContext(DbContextOptions<WorxForgeDbContext> options) : DbContext(options)
{
}
