using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications.Payloads;

public record ProduceNotificationPayload(SubscribableTopics Topic, Guid UserId, Guid EventId);
