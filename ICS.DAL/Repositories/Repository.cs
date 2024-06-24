using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using ICS.DAL.Mappers;

namespace ICS.DAL.Repositories;

public class Repository<TEntity>(
    DbContext dbContext,
    IEntityMapper<TEntity> entityMapper)
    : IRepository<TEntity>
    where TEntity : class, IEntity
{
    // private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    //
    // public IQueryable<TEntity> Get() => _dbSet;
    //
    // public async Task<IEnumerable<TEntity>> GetAllAsync()
    // {
    //     return await _dbSet.ToListAsync();
    // }
    //
    // public async ValueTask<bool> ExistsAsync(TEntity entity)
    //     => entity.Id != Guid.Empty
    //        && await _dbSet.AnyAsync(e => e.Id == entity.Id);
    //
    // public async Task AddAsync(TEntity entity)
    // {
    //     await _dbSet.AddAsync(entity);
    //     await dbContext.SaveChangesAsync();
    // }
    //
    // public TEntity Insert(TEntity entity)
    //     => _dbSet.Add(entity).Entity;
    //
    // public async Task UpdateAsync(TEntity entity)
    // {
    //     _dbSet.Update(entity);
    //     await dbContext.SaveChangesAsync();
    // }
    //
    // public async Task DeleteAsync(Guid entityId)
    //     => _dbSet.Remove(await _dbSet.SingleAsync(i => i.Id == entityId).ConfigureAwait(false));
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public IQueryable<TEntity> Get() => _dbSet;

    public async ValueTask<bool> ExistsAsync(TEntity entity)
        => entity.Id != Guid.Empty
           && await _dbSet.AnyAsync(e => e.Id == entity.Id).ConfigureAwait(false);

    public TEntity Insert(TEntity entity)
        => _dbSet.Add(entity).Entity;

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity existingEntity = await _dbSet.SingleAsync(e => e.Id == entity.Id);
        entityMapper.MapToExistingEntity(existingEntity, entity);
        return existingEntity;
    }

    public async Task DeleteAsync(Guid entityId)
        => _dbSet.Remove(await _dbSet.SingleAsync(i => i.Id == entityId).ConfigureAwait(false));
}