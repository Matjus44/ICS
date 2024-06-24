using System.Collections.ObjectModel;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class ActivityModelMapper(IRatingModelMapper ratingModelMapper) 
    : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>, IActivityModelMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity) 
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                Id = entity.Id,
                SubjectId = entity.SubjectId,
                Start = entity.Start,
                Finish = entity.Finish,
                Room = entity.Room,
                Type = entity.Type,
                ActivityDescription = entity.ActivityDescription
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                SubjectId = entity.SubjectId,
                Start = entity.Start,
                Finish = entity.Finish,
                Room = entity.Room,
                Type = entity.Type,
                ActivityDescription = entity.ActivityDescription,
                Ratings = ratingModelMapper.MapToListModel(entity.Ratings)
                    .ToObservableCollection()
            };

    // existing
    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            SubjectId = model.SubjectId,
            Start = model.Start,
            Finish = model.Finish,
            Room = model.Room,
            Type = model.Type,
            ActivityDescription = model.ActivityDescription
        };

    public ActivityListModel MapToListModel(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            SubjectId = model.SubjectId,
            Start = model.Start,
            Finish = model.Finish,
            Room = model.Room,
            Type = model.Type,
            ActivityDescription = model.ActivityDescription
        };

    // new
    public override ActivityEntity MapToEntity(ActivityDetailModel model, Guid id)
        => new()
        {
            Id = id,
            SubjectId = model.SubjectId,
            Start = model.Start,
            Finish = model.Finish,
            Room = model.Room,
            Type = model.Type,
            ActivityDescription = model.ActivityDescription,
        };

    public ActivityEntity MapToEntity(ActivityListModel model)
        => new()
        {
            Id = model.Id,
            SubjectId = model.SubjectId,
            Start = model.Start,
            Finish = model.Finish,
            Room = model.Room,
            Type = model.Type,
            ActivityDescription = model.ActivityDescription,
            Subject = null
        };

    public ObservableCollection<ActivityListModel> MapToListModelCollection(ICollection<ActivityEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        
        var activityListModels = entities
            .Select(MapToListModel)
            .ToList();

        return new ObservableCollection<ActivityListModel>(activityListModels);
    }

    public ICollection<ActivityEntity> MapToEntityCollection(ObservableCollection<ActivityListModel> models)
    {
        if (models is null)
        {
            return [];
        }
        
        var activityEntities = models
            .Select(MapToEntity)
            .ToList();

        return activityEntities;
    }
}