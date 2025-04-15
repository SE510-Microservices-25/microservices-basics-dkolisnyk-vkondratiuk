using Notifications.Business.Common.Persistence;
using Notifications.Business.Notifications.Core;

namespace Notifications.Business.Notifications;

public interface INotificationOutboxRepository : IRepository<NotificationOutbox>
{
}
