using Moq;
using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Entities;
using WorxForge.Domain.Enums;
using WorxForge.Domain.Exceptions;
using WorxForge.Infrastructure.Services;

namespace WorxForge.Infrastructure.UnitTests;

public sealed class ReactionServiceTests
{
    private readonly Guid userId = Guid.NewGuid();
    private readonly Asset asset = new() { AssetId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), Title = "Reusable prompt" };
    private readonly Mock<IAssetRepository> assets = new();
    private readonly Mock<IReactionRepository> reactions = new();
    private readonly Mock<IUserRepository> users = new();
    private readonly Mock<INotificationService> notifications = new();

    public ReactionServiceTests()
    {
        assets.Setup(repo => repo.GetByIdAsync(asset.AssetId, It.IsAny<CancellationToken>())).ReturnsAsync(asset);
        users.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User { UserId = userId, DisplayName = "Contributor" });
        reactions.Setup(repo => repo.GetSummaryAsync(asset.AssetId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AssetReactionDto { AssetId = asset.AssetId, LikeCount = 1, TotalCount = 1, CurrentUserReaction = ReactionTypes.Like });
    }

    private ReactionService Service => new(assets.Object, reactions.Object, users.Object, notifications.Object);

    [Theory]
    [InlineData(null)]
    [InlineData("Dislike")]
    public async Task Useful_AddsOrReplacesLegacyFeedback_AndNotifiesOwner(string? previousReaction)
    {
        if (previousReaction is not null)
            reactions.Setup(repo => repo.GetAsync(userId, asset.AssetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Reaction { UserId = userId, AssetId = asset.AssetId, ReactionType = previousReaction });

        var result = await Service.ReactToAssetAsync(userId, asset.AssetId, "like", CancellationToken.None);

        Assert.Equal(1, result.LikeCount);
        reactions.Verify(repo => repo.UpsertAsync(It.Is<Reaction>(reaction =>
            reaction.UserId == userId && reaction.AssetId == asset.AssetId && reaction.ReactionType == ReactionTypes.Like),
            CancellationToken.None), Times.Once);
        reactions.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        notifications.Verify(service => service.SendAsync(asset.AuthorId, userId, NotificationType.PostLiked,
            It.Is<string>(message => message.Contains("useful") && !message.Contains("disliked")), asset.AssetId, null,
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Useful_ClickAgain_RemovesFeedback_WithoutAnotherNotification()
    {
        reactions.Setup(repo => repo.GetAsync(userId, asset.AssetId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Reaction { UserId = userId, AssetId = asset.AssetId, ReactionType = ReactionTypes.Like });
        await Service.ReactToAssetAsync(userId, asset.AssetId, ReactionTypes.Like, CancellationToken.None);
        reactions.Verify(repo => repo.DeleteAsync(userId, asset.AssetId, CancellationToken.None), Times.Once);
        reactions.Verify(repo => repo.UpsertAsync(It.IsAny<Reaction>(), It.IsAny<CancellationToken>()), Times.Never);
        notifications.Verify(service => service.SendAsync(It.IsAny<Guid>(), It.IsAny<Guid?>(), It.IsAny<NotificationType>(),
            It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Useful_OnOwnAsset_DoesNotNotifySelf()
    {
        asset.AuthorId = userId;
        await Service.ReactToAssetAsync(userId, asset.AssetId, ReactionTypes.Like, CancellationToken.None);
        Assert.Empty(notifications.Invocations);
    }

    [Fact]
    public async Task Dislike_IsRejected_WithoutChangingData()
    {
        await Assert.ThrowsAsync<RequestValidationException>(() =>
            Service.ReactToAssetAsync(userId, asset.AssetId, "Dislike", CancellationToken.None));
        Assert.Empty(reactions.Invocations);
        Assert.Empty(notifications.Invocations);
    }

    [Fact]
    public async Task Useful_OnMissingAsset_ReturnsNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(() =>
            Service.ReactToAssetAsync(userId, Guid.NewGuid(), ReactionTypes.Like, CancellationToken.None));
        Assert.Empty(reactions.Invocations);
    }

    [Fact]
    public async Task ExplicitRemoval_DeletesReaction_AndReturnsServerSummary()
    {
        var result = await Service.RemoveAssetReactionAsync(userId, asset.AssetId, CancellationToken.None);
        Assert.Equal(asset.AssetId, result.AssetId);
        reactions.Verify(repo => repo.DeleteAsync(userId, asset.AssetId, CancellationToken.None), Times.Once);
    }
}
