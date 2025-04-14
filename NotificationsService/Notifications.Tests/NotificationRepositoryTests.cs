using Microsoft.EntityFrameworkCore;
using Notifications.Business.Notifications.Core;
using Notifications.Data.Contexts;
using Notifications.Data.Repositories;
using Subscriptions.Business.Subscriptions.Core;
using Xunit;

namespace Notifications.Tests;

public class NotificationRepositoryTests
{
    private readonly DbContextOptions<NotificationsContext> _contextOptions;

    public NotificationRepositoryTests()
    {
        _contextOptions = new DbContextOptionsBuilder<NotificationsContext>()
            .UseInMemoryDatabase(databaseName: $"NotificationsTestDb_{Guid.NewGuid()}")
            .Options;
    }

    [Fact]
    public async Task ListAll_ShouldReturnAllNotifications()
    {
        // Arrange
        await using var context = new NotificationsContext(_contextOptions);
        var repository = new NotificationRepository(context);
        
        var notifications = new List<Notification>
        {
            new() { Id = Guid.NewGuid(), Topic = SubscribableTopics.NEW_FOLLOWER, Message = "New follower notification" },
            new() { Id = Guid.NewGuid(), Topic = SubscribableTopics.SPECIAL_OFFER, Message = "Special offer notification" }
        };

        await context.Notifications.AddRangeAsync(notifications);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ListAll();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(notifications.Select(n => n.Id), result.Select(n => n.Id));
        Assert.Equal(notifications.Select(n => n.Topic), result.Select(n => n.Topic));
        Assert.Equal(notifications.Select(n => n.Message), result.Select(n => n.Message));
    }

    [Fact]
    public async Task Create_ShouldAddNotificationToDatabase()
    {
        // Arrange
        await using var context = new NotificationsContext(_contextOptions);
        var repository = new NotificationRepository(context);
        
        var notification = new Notification 
        { 
            Id = Guid.NewGuid(), 
            Topic = SubscribableTopics.NEW_FOLLOWER, 
            Message = "Test notification" 
        };

        // Act
        await repository.Create(notification);

        // Assert
        var savedNotification = await context.Notifications.FindAsync(notification.Id);
        Assert.NotNull(savedNotification);
        Assert.Equal(notification.Id, savedNotification.Id);
        Assert.Equal(notification.Topic, savedNotification.Topic);
        Assert.Equal(notification.Message, savedNotification.Message);
    }

    [Fact]
    public async Task Delete_ShouldRemoveNotificationFromDatabase()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        
        await using (var arrangeContext = new NotificationsContext(_contextOptions))
        {
            var notification = new Notification 
            { 
                Id = notificationId, 
                Topic = SubscribableTopics.NEW_FOLLOWER, 
                Message = "Test notification" 
            };
            
            await arrangeContext.Notifications.AddAsync(notification);
            await arrangeContext.SaveChangesAsync();
        }

        // Act
        await using (var actContext = new NotificationsContext(_contextOptions))
        {
            var repository = new NotificationRepository(actContext);
            await repository.Delete(notificationId);
        }

        // Assert
        await using (var assertContext = new NotificationsContext(_contextOptions))
        {
            var deletedNotification = await assertContext.Notifications.FindAsync(notificationId);
            Assert.Null(deletedNotification);
        }
    }
} 