using Moq;
using Subscriptions.Business.Subscriptions;
using Subscriptions.Business.Subscriptions.Core;
using Xunit;

namespace Subscriptions.Tests;

public class SubscriptionsServiceTests
{
    private readonly Mock<ISubscriptionRepository> _mockRepository;
    private readonly SubscriptionsService _service;

    public SubscriptionsServiceTests()
    {
        _mockRepository = new Mock<ISubscriptionRepository>();
        _service = new SubscriptionsService(_mockRepository.Object);
    }

    [Fact]
    public async Task ListAll_ShouldReturnAllSubscriptions()
    {
        // Arrange
        var expectedSubscriptions = new List<Subscription>
        {
            new Subscription { Id = Guid.NewGuid(), Topic = SubscribableTopics.NEW_FOLLOWER, UserId = Guid.NewGuid() },
            new Subscription { Id = Guid.NewGuid(), Topic = SubscribableTopics.SPECIAL_OFFER, UserId = Guid.NewGuid() }
        };

        _mockRepository.Setup(repo => repo.ListAll())
            .ReturnsAsync(expectedSubscriptions);

        // Act
        var result = await _service.ListAll();

        // Assert
        Assert.Equal(expectedSubscriptions.Count, result.Count());
        Assert.Equal(expectedSubscriptions, result);
        _mockRepository.Verify(repo => repo.ListAll(), Times.Once);
    }

    [Fact]
    public async Task Subscribe_ShouldCreateSubscriptionAndReturnId()
    {
        // Arrange
        var topic = SubscribableTopics.NEW_FOLLOWER;
        Subscription capturedSubscription = null;

        _mockRepository.Setup(repo => repo.Create(It.IsAny<Subscription>()))
            .Callback<Subscription>(subscription => capturedSubscription = subscription)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.Subscribe(topic);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        _mockRepository.Verify(repo => repo.Create(It.IsAny<Subscription>()), Times.Once);
        Assert.NotNull(capturedSubscription);
        Assert.Equal(topic, capturedSubscription.Topic);
        Assert.Equal(result, capturedSubscription.Id);
        Assert.NotEqual(Guid.Empty, capturedSubscription.UserId);
    }

    [Fact]
    public async Task Unsubscribe_ShouldDeleteSubscription()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.Delete(subscriptionId))
            .Returns(Task.CompletedTask);

        // Act
        await _service.Unsubscribe(subscriptionId);

        // Assert
        _mockRepository.Verify(repo => repo.Delete(subscriptionId), Times.Once);
    }
} 