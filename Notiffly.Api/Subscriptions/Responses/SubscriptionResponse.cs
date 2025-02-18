using Notiffly.Business.Subscriptions.Core;

namespace Notiffly.Api.Subscriptions.Responses;

public class SubscriptionResponse
{
    public Guid Id { get; set; }
    
    public SubscribableTopics Topic { get; set; }
}
