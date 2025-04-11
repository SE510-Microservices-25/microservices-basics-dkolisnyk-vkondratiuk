using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications;

public interface INotificationRemoteService
{
    Task Produce(SubscribableTopics topic, Guid userId);
}
