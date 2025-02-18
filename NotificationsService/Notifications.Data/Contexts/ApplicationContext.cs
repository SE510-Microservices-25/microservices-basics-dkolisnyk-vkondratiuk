using Microsoft.EntityFrameworkCore;
using Notifications.Business.Notifications.Core;

namespace Notifications.Data.Contexts;

public class NotificationsContext : DbContext
{
    public DbSet<Notification> Notifications => Set<Notification>();

    public NotificationsContext(DbContextOptions<NotificationsContext> options)
        : base(options)
    {
    }
}
