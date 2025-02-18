using Microsoft.EntityFrameworkCore;
using Subscriptions.Business.Subscriptions;
using Subscriptions.Business.Subscriptions.Core;
using Subscriptions.Data.Contexts;

namespace Subscriptions.Data.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationContext _applicationContext;

    public SubscriptionRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<IEnumerable<Subscription>> ListAll()
    {
        return await _applicationContext.Subscriptions.ToListAsync();
    }

    public async Task Create(Subscription entity)
    {
        await _applicationContext.Subscriptions.AddAsync(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = new Subscription() { Id = id };

        _applicationContext.Subscriptions.Attach(entity);
        _applicationContext.Subscriptions.Remove(entity);

        await _applicationContext.SaveChangesAsync();
    }
}
