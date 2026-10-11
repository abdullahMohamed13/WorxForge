using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Entities;
using WorxForge.Domain.Enums;
using WorxForge.Domain.Errors;
using WorxForge.Domain.Exceptions;

namespace WorxForge.Infrastructure.Services;

public sealed class ReactionService(
    IAssetRepository assetRepository,
    IReactionRepository reactionRepository,
    IUserRepository userRepository,
    INotificationService notificationService) : IReactionService
{
    public async Task<AssetReactionDto> ReactToAssetAsync(Guid userId, Guid assetId, string reactionType, CancellationToken cancellationToken)
    {
        if (!ReactionTypes.IsSupported(reactionType))
        {
            throw new RequestValidationException(new Dictionary<string, string[]>
            {
                [nameof(reactionType)] = ["Only Useful (Like) feedback is supported."]
            });
        }

        var asset = await assetRepository.GetByIdAsync(assetId, cancellationToken);
        if (asset is null)
        {
            throw new NotFoundException(Errors.AssetNotFound);
        }

        var normalizedReactionType = ReactionTypes.Normalize(reactionType);
        var existingReaction = await reactionRepository.GetAsync(userId, assetId, cancellationToken);

        if (existingReaction is not null && string.Equals(existingReaction.ReactionType, normalizedReactionType, StringComparison.Ordinal))
        {
            await reactionRepository.DeleteAsync(userId, assetId, cancellationToken);
        }
        else
        {
            await reactionRepository.UpsertAsync(new Reaction
            {
                AssetId = assetId,
                UserId = userId,
                ReactionType = normalizedReactionType
            }, cancellationToken);

            await NotifyAssetOwnerAsync(asset, userId, cancellationToken);
        }

        return await reactionRepository.GetSummaryAsync(assetId, userId, cancellationToken);
    }

    public async Task<AssetReactionDto> RemoveAssetReactionAsync(Guid userId, Guid assetId, CancellationToken cancellationToken)
    {
        var asset = await assetRepository.GetByIdAsync(assetId, cancellationToken);
        if (asset is null)
        {
            throw new NotFoundException(Errors.AssetNotFound);
        }

        await reactionRepository.DeleteAsync(userId, assetId, cancellationToken);
        return await reactionRepository.GetSummaryAsync(assetId, userId, cancellationToken);
    }

    private async Task NotifyAssetOwnerAsync(Asset asset, Guid actorUserId, CancellationToken cancellationToken)
    {
        if (asset.AuthorId == actorUserId)
        {
            return;
        }

        var actor = await userRepository.GetByIdAsync(actorUserId, cancellationToken);
        var actorDisplayName = actor?.DisplayName ?? "Someone";

        await notificationService.SendAsync(
            recipientUserId: asset.AuthorId,
            actorUserId: actorUserId,
            type: NotificationType.PostLiked,
            message: $"{actorDisplayName} found your asset \"{asset.Title}\" useful.",
            assetId: asset.AssetId,
            commentId: null,
            cancellationToken: cancellationToken);
    }
}
