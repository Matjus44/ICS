using ICS.DAL.Entities;

namespace ICS.DAL.Mappers;

public interface IEntityMapper<TEntity>
    where TEntity : class, IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity updatedEntity);
}
