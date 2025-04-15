using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Business.Subscriptions;
using Subscriptions.Business.Subscriptions.Core;
using Subscriptions.Data.Contexts;

namespace Subscriptions.Data.Repositories;

public class OutboxMessageRepository : IOutboxMessageRepository
{
    private readonly ApplicationContext _applicationContext;

    public OutboxMessageRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<IEnumerable<OutboxMessage>> ListAll()
    {
        return await _applicationContext.OutboxMessages.ToListAsync();
    }

    public async Task SaveChanges()
    {
        await _applicationContext.SaveChangesAsync();
    }
}
