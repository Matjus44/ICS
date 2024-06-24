using System.Collections.ObjectModel;
using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers.Interfaces;

public interface IActivityModelMapper
    : IModelMapper<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    ActivityListModel MapToListModel(ActivityDetailModel detailModel);
    ActivityEntity MapToEntity(ActivityListModel model);
    ObservableCollection<ActivityListModel> MapToListModelCollection(ICollection<ActivityEntity> entities);
    ICollection<ActivityEntity> MapToEntityCollection(ObservableCollection<ActivityListModel> models);
}