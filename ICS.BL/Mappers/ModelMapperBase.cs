using ICS.BL.Mappers.Interfaces;

namespace ICS.BL.Mappers;

public abstract class 
    ModelMapperBase <TEntity, TListModel, TDetailModel> : IModelMapper<TEntity, TListModel, TDetailModel>
{
    public abstract TListModel MapToListModel(TEntity? entity);
    
    public IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
        => entities.Select(MapToListModel);
    
    public abstract TDetailModel MapToDetailModel(TEntity entity);
    public abstract TEntity MapToEntity(TDetailModel model);
    public abstract TEntity MapToEntity(TDetailModel model, Guid id);
}