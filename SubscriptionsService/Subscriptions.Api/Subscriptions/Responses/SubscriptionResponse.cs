using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Api.Subscriptions.Responses;

public class SubscriptionResponse
{
    public Guid Id { get; set; }
    
    public SubscribableTopics Topic { get; set; }
}
