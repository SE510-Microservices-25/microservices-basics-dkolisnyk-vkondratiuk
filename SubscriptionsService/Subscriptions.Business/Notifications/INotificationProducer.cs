using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications;

public interface INotificationProducer
{
    Task Produce(SubscribableTopics topic, Guid userId);
}
