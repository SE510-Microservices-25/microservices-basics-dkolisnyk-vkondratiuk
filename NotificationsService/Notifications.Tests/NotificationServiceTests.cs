using Moq;
using Notifications.Business.Notifications;
using Subscriptions.Business.Subscriptions.Core;
using Xunit;

namespace Notifications.Tests;

public class NotificationServiceTests
{
    private readonly NotificationService _service;

    public NotificationServiceTests()
    {
        _service = new NotificationService();
    }

    [Fact]
    public async Task Produce_ShouldNotThrowException()
    {
        // Arrange
        var topic = SubscribableTopics.NEW_FOLLOWER;
        var userId = Guid.NewGuid();

        // Act & Assert
        await _service.Produce(topic, userId);
    }

    [Theory]
    [InlineData(SubscribableTopics.NEW_FOLLOWER)]
    [InlineData(SubscribableTopics.SPECIAL_OFFER)]
    public async Task Produce_ShouldAcceptDifferentTopics(SubscribableTopics topic)
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act & Assert
        await _service.Produce(topic, userId);
    }
} 