using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Subscriptions;

public class SubscriptionsService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionsService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IEnumerable<Subscription>> ListAll()
    {
        return await _subscriptionRepository.ListAll();
    }

    public async Task<Guid> Subscribe(SubscribableTopics topic)
    {
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            Topic = topic,
            UserId = Guid.NewGuid(), // temp mock value
        };

        await _subscriptionRepository.Create(subscription);

        return subscription.Id;
    }

    public async Task Unsubscribe(Guid id)
    {
        await _subscriptionRepository.Delete(id);
    }
}
