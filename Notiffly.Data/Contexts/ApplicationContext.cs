using Microsoft.EntityFrameworkCore;
using Notiffly.Business.Subscriptions.Core;

namespace Notiffly.Data.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}
