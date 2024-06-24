using ICS.DAL.Entities;

namespace ICS.DAL.Repositories;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    // IQueryable<TEntity> Get();
    // Task<IEnumerable<TEntity>> GetAllAsync();
    // ValueTask<bool> ExistsAsync(TEntity entity);
    // Task AddAsync(TEntity entity);
    // TEntity Insert(TEntity entity);
    // Task UpdateAsync(TEntity entity);
    // Task DeleteAsync(Guid entityId);
    
    IQueryable<TEntity> Get();
    Task DeleteAsync(Guid entityId);
    ValueTask<bool> ExistsAsync(TEntity entity);
    TEntity Insert(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}