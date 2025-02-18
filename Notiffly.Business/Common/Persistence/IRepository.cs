namespace Notiffly.Business.Common.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> ListAll();

    Task Create(TEntity entity);
    
    Task Delete(Guid id);
}
