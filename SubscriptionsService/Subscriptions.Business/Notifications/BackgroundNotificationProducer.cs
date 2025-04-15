using Subscriptions.Business.Common.MessageBroker;
using Subscriptions.Business.Notifications.Payloads;
using Subscriptions.Business.Subscriptions;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications;

public class BackgroundNotificationProducer
{
    private readonly IMessageBroker _messageBroker;
    private readonly IOutboxMessageRepository _outboxMessageRepository;

    public BackgroundNotificationProducer(IOutboxMessageRepository outboxMessageRepository,
        IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
        _outboxMessageRepository = outboxMessageRepository;
    }

    public async Task ProcessOutbox()
    {
        var messages = (await _outboxMessageRepository.ListAll()).Where(x => x.Processed == false);

        foreach (var unprocessed in messages)
        {
            _messageBroker.Publish(
                new ProduceNotificationPayload(SubscribableTopics.SUBSCRIPTION_CREATED, new Guid(), new Guid())
            );
            unprocessed.Processed = true;
        }

        await _outboxMessageRepository.SaveChanges();
    }
}
