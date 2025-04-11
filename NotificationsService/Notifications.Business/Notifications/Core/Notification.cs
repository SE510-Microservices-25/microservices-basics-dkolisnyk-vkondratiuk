using Subscriptions.Business.Subscriptions.Core;

namespace Notifications.Business.Notifications.Core;

public class Notification
{
    public Guid Id { get; set; }
    public SubscribableTopics Topic { get; set; }
    public string? Message { get; set; }
}
