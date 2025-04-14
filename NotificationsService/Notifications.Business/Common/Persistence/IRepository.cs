namespace Notifications.Business.Common.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> ListAll();
    Task<TEntity?> GetById(Guid id);
    Task Create(TEntity entity);
    Task Delete(Guid id);
}
