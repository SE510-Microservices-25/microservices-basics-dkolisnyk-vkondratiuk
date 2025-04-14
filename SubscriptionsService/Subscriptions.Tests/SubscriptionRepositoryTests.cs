using Microsoft.EntityFrameworkCore;
using Subscriptions.Business.Subscriptions.Core;
using Subscriptions.Data.Contexts;
using Subscriptions.Data.Repositories;
using Xunit;

namespace Subscriptions.Tests;

public class SubscriptionRepositoryTests
{
    private readonly DbContextOptions<ApplicationContext> _contextOptions;

    public SubscriptionRepositoryTests()
    {
        _contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: $"SubscriptionsTestDb_{Guid.NewGuid()}")
            .Options;
    }

    [Fact]
    public async Task ListAll_ShouldReturnAllSubscriptions()
    {
        // Arrange
        await using var context = new ApplicationContext(_contextOptions);
        var repository = new SubscriptionRepository(context);
        
        var subscriptions = new List<Subscription>
        {
            new() { Id = Guid.NewGuid(), Topic = SubscribableTopics.NEW_FOLLOWER, UserId = Guid.NewGuid() },
            new() { Id = Guid.NewGuid(), Topic = SubscribableTopics.SPECIAL_OFFER, UserId = Guid.NewGuid() }
        };

        await context.Subscriptions.AddRangeAsync(subscriptions);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ListAll();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(subscriptions.Select(s => s.Id), result.Select(s => s.Id));
        Assert.Equal(subscriptions.Select(s => s.Topic), result.Select(s => s.Topic));
        Assert.Equal(subscriptions.Select(s => s.UserId), result.Select(s => s.UserId));
    }

    [Fact]
    public async Task Create_ShouldAddSubscriptionToDatabase()
    {
        // Arrange
        await using var context = new ApplicationContext(_contextOptions);
        var repository = new SubscriptionRepository(context);
        
        var subscription = new Subscription 
        { 
            Id = Guid.NewGuid(), 
            Topic = SubscribableTopics.NEW_FOLLOWER, 
            UserId = Guid.NewGuid() 
        };

        // Act
        await repository.Create(subscription);

        // Assert
        var savedSubscription = await context.Subscriptions.FindAsync(subscription.Id);
        Assert.NotNull(savedSubscription);
        Assert.Equal(subscription.Id, savedSubscription.Id);
        Assert.Equal(subscription.Topic, savedSubscription.Topic);
        Assert.Equal(subscription.UserId, savedSubscription.UserId);
    }

    [Fact]
    public async Task Delete_ShouldRemoveSubscriptionFromDatabase()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        
        await using (var arrangeContext = new ApplicationContext(_contextOptions))
        {
            var subscription = new Subscription 
            { 
                Id = subscriptionId, 
                Topic = SubscribableTopics.NEW_FOLLOWER, 
                UserId = Guid.NewGuid() 
            };
            
            await arrangeContext.Subscriptions.AddAsync(subscription);
            await arrangeContext.SaveChangesAsync();
        }

        // Act
        await using (var actContext = new ApplicationContext(_contextOptions))
        {
            var repository = new SubscriptionRepository(actContext);
            await repository.Delete(subscriptionId);
        }

        // Assert
        await using (var assertContext = new ApplicationContext(_contextOptions))
        {
            var deletedSubscription = await assertContext.Subscriptions.FindAsync(subscriptionId);
            Assert.Null(deletedSubscription);
        }
    }
} 