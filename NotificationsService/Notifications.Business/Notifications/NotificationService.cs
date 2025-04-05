using Notifications.Business.Notifications.Core;

namespace Notifications.Business.Notifications;

public class NotificationService
{
    private readonly INotificationRepository _notificationRepo;

    public NotificationService(INotificationRepository notificationRepo)
    {
        _notificationRepo = notificationRepo;
    }

    public async Task<IEnumerable<Notification>> ListAll()
    {
        return await _notificationRepo.ListAll();
    }

    public async Task<Guid> Create(NotifiableTopics topic, string? message)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Topic = topic,
            Message = message
        };

        await _notificationRepo.Create(notification);
        return notification.Id;
    }

    public async Task Delete(Guid id)
    {
        await _notificationRepo.Delete(id);
    }
}
