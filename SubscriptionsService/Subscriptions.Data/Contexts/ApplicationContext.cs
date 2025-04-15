using Microsoft.EntityFrameworkCore;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Data.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<OutboxMessage> OutboxMessages=> Set<OutboxMessage>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}
