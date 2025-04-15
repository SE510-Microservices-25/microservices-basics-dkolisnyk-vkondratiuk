using Subscriptions.Business.Common.MessageBroker;
using Subscriptions.Business.Notifications.Payloads;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications;

public class MockNotificationProducer : INotificationProducer
{
    private readonly IMessageBroker _messageBroker;

    public MockNotificationProducer(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    public async Task Produce(SubscribableTopics topic, Guid userId)
    {
        await _messageBroker.Publish(new ProduceNotificationPayload(topic, userId, new Guid()));
    }
}
