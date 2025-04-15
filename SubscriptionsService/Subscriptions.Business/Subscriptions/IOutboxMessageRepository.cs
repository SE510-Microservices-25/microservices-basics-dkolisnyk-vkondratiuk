using Subscriptions.Business.Common.Persistence;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Subscriptions;

public interface IOutboxMessageRepository
{
    Task<IEnumerable<OutboxMessage>> ListAll();

    Task SaveChanges();
}
