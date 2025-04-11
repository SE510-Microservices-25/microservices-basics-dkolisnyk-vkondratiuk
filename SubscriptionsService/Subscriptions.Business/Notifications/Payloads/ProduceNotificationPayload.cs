using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications.Payloads;

public sealed class ProduceNotificationPayload
{
    public SubscribableTopics Topic { get; set; }

    public Guid UserId { get; set; }
}
