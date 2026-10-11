using Microsoft.EntityFrameworkCore;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Entities;
using WorxForge.Domain.Enums;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Persistence.Seed;

public sealed class GuestUserSeeder : IDataSeeder
{
    public async Task<int> SeedAsync(WorxForgeDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Users.AnyAsync(user => user.UserId == GuestWorkspaceConstants.UserId, cancellationToken))
        {
            return 0;
        }

        // The existing foreign keys need an author/user row, not a real login account.
        // TODO: Login is skipped pending adding an SMTP account and completing account flows.
        var now = DateTime.UtcNow;
        context.Users.Add(new User
        {
            UserId = GuestWorkspaceConstants.UserId,
            Username = GuestWorkspaceConstants.Username,
            DisplayName = GuestWorkspaceConstants.DisplayName,
            Email = string.Empty,
            EmailVerified = false,
            GoogleId = null,
            Role = UserRole.User.ToString(),
            CreatedAt = now,
            UpdatedAt = now
        });
        await context.SaveChangesAsync(cancellationToken);
        return 1;
    }
}
