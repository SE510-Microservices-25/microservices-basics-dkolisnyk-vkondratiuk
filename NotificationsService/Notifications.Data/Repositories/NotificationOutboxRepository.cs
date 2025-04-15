using Microsoft.EntityFrameworkCore;
using Notifications.Business.Notifications;
using Notifications.Business.Notifications.Core;
using Notifications.Data.Contexts;

namespace Notifications.Data.Repositories;

public class NotificationOutboxRepository : INotificationOutboxRepository
{
    private readonly NotificationsContext _context;

    public NotificationOutboxRepository(NotificationsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationOutbox>> ListAll()
    {
        return await _context.NotificationOutboxes.ToListAsync();
    }

    public async Task Create(NotificationOutbox entity)
    {
        _context.NotificationOutboxes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid eventId)
    {
        var outbox = new NotificationOutbox { Id = eventId };
        _context.NotificationOutboxes.Attach(outbox);
        _context.NotificationOutboxes.Remove(outbox);
        await _context.SaveChangesAsync();
    }
}
