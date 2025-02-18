namespace Notiffly.Business.Subscriptions.Core;

public class Subscription
{
    public Guid Id { get; set; }
    
    public SubscribableTopics Topic { get; set; }

    public Guid UserId { get; set; }
}
