using Subscriptions.Business.Subscriptions.Core;

namespace Notifications.Business.Notifications;

public class NotificationService
{
    public async Task Produce(SubscribableTopics topic, Guid userId)
    {
        // TODO:
        // 1. Pick appropriate template
        // 2. Pull contact information by userId
        // 3. Send email
        
        Console.WriteLine($"Producing a notification in topic={topic} for userId={userId}");
    }
}
