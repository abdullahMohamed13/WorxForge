using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using WorxForge.Core.Interfaces.Notifications;
using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Notifications;
using WorxForge.Domain.Entities;
using WorxForge.Domain.Enums;
using WorxForge.Infrastructure.Services;

namespace WorxForge.Infrastructure.UnitTests;

public sealed class NotificationDeliveryTests
{
    [Fact]
    public async Task SendAsync_PersistsAndQueuesInAppNotification_WithoutSmtpConfiguration()
    {
        var recipient = Guid.NewGuid();
        var repository = new Mock<INotificationRepository>();
        repository.Setup(repo => repo.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var queue = new Mock<INotificationQueue>();
        var service = new NotificationService(repository.Object, queue.Object,
            new DisabledNotificationEmailSender(), NullLogger<NotificationService>.Instance);

        await service.SendAsync(recipient, Guid.NewGuid(), NotificationType.Announcement,
            "Community update", null, null, CancellationToken.None);

        repository.Verify(repo => repo.AddAsync(It.Is<Notification>(notification =>
            notification.RecipientUserId == recipient && notification.Message == "Community update"),
            CancellationToken.None), Times.Once);
        queue.Verify(item => item.QueueNotification(It.Is<NotificationDeliveryMessage>(notification =>
            notification.RecipientUserId == recipient && notification.Message == "Community update")), Times.Once);
    }
    [Fact]
    public async Task RetiredNegativeFeedback_IsNotPersistedOrDelivered()
    {
        var repository = new Mock<INotificationRepository>();
        var queue = new Mock<INotificationQueue>();
        var service = new NotificationService(repository.Object, queue.Object,
            new DisabledNotificationEmailSender(), NullLogger<NotificationService>.Instance);
        await service.SendAsync(Guid.NewGuid(), Guid.NewGuid(), NotificationType.PostDisliked,
            "Historical negative feedback", null, null, CancellationToken.None);
        Assert.Empty(repository.Invocations);
        Assert.Empty(queue.Invocations);
    }

}
