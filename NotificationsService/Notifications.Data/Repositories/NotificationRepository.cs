using Microsoft.EntityFrameworkCore;
using Notifications.Business.Notifications;
using Notifications.Business.Notifications.Core;
using Notifications.Data.Contexts;

namespace Notifications.Data.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationsContext _context;

    public NotificationRepository(NotificationsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> ListAll()
    {
        return await _context.Notifications.ToListAsync();
    }

    public async Task Create(Notification entity)
    {
        _context.Notifications.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var notification = new Notification { Id = id };
        _context.Notifications.Attach(notification);
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
    }
}
