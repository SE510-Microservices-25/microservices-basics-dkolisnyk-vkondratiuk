using MassTransit;
using Notifications.Api.Notifications.Payloads;
using Notifications.Business.Notifications;
using Notifications.Business.Notifications.Core;

namespace Notifications.Api.Notifications;

public sealed class NotificationsConsumer : IConsumer<ProduceNotificationPayload>
{
    private readonly NotificationService _notificationService;
    private readonly INotificationOutboxRepository _notificationOutboxRepository;

    public NotificationsConsumer(NotificationService notificationService,
        INotificationOutboxRepository notificationOutboxRepository)
    {
        _notificationService = notificationService;
        _notificationOutboxRepository = notificationOutboxRepository;
    }

    public async Task Consume(ConsumeContext<ProduceNotificationPayload> context)
    {
        var skipDuplicate = await ShouldSkipDuplicate(context.Message.EventId);

        if (skipDuplicate)
        {
            Console.WriteLine("Skipping duplicate processing...");
            return;
        }

        Console.WriteLine("Processing notification event...");
        await _notificationService.Produce(context.Message.Topic, context.Message.UserId);
        Console.WriteLine("Finished processing notification event");

        await SaveConsumed(context.Message.EventId);
    }


    private async Task<bool> ShouldSkipDuplicate(Guid eventId)
    {
        var list = await _notificationOutboxRepository.ListAll();

        var match = list.Where(x => x.Id == eventId);

        if (match == null)
        {
            return false;
        }

        return true;
    }

    private async Task SaveConsumed(Guid eventId)
    {
        await _notificationOutboxRepository.Create(new NotificationOutbox { Id = eventId });
    }
}
