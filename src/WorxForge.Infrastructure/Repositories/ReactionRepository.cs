using Microsoft.EntityFrameworkCore;
using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Entities;
using WorxForge.Infrastructure.Configuration;

namespace WorxForge.Infrastructure.Repositories;

public sealed class ReactionRepository(WorxForgeDbContext dbContext) : IReactionRepository
{
    public Task<Reaction?> GetAsync(Guid userId, Guid assetId, CancellationToken cancellationToken) =>
        dbContext.Reactions
            .AsNoTracking()
            .SingleOrDefaultAsync(reaction => reaction.UserId == userId && reaction.AssetId == assetId, cancellationToken);

    public Task<int> CountByAssetAsync(Guid assetId, CancellationToken cancellationToken) =>
        dbContext.Reactions.CountAsync(reaction => reaction.AssetId == assetId && reaction.ReactionType == ReactionTypes.Like, cancellationToken);

    public async Task<AssetReactionDto> GetSummaryAsync(Guid assetId, Guid userId, CancellationToken cancellationToken)
    {
        var reactions = await dbContext.Reactions
            .AsNoTracking()
            .Where(reaction => reaction.AssetId == assetId && reaction.ReactionType == ReactionTypes.Like)
            .ToListAsync(cancellationToken);

        var currentUserReaction = reactions.Any(reaction => reaction.UserId == userId) ? ReactionTypes.Like : null;
        var likeCount = reactions.Count;

        return new AssetReactionDto
        {
            AssetId = assetId,
            LikeCount = likeCount,
            TotalCount = reactions.Count,
            CurrentUserReaction = currentUserReaction
        };
    }

    public async Task UpsertAsync(Reaction reaction, CancellationToken cancellationToken)
    {
        var existingReaction = await dbContext.Reactions
            .SingleOrDefaultAsync(existing => existing.UserId == reaction.UserId && existing.AssetId == reaction.AssetId, cancellationToken);

        if (existingReaction is null)
        {
            await dbContext.Reactions.AddAsync(reaction, cancellationToken);
        }
        else
        {
            existingReaction.ReactionType = reaction.ReactionType;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid userId, Guid assetId, CancellationToken cancellationToken)
    {
        var existingReaction = await dbContext.Reactions
            .SingleOrDefaultAsync(reaction => reaction.UserId == userId && reaction.AssetId == assetId, cancellationToken);

        if (existingReaction is null)
        {
            return;
        }

        dbContext.Reactions.Remove(existingReaction);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
