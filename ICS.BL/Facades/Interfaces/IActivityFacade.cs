using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<IEnumerable<ActivityDetailModel>> SortActivitiesAsync(string sortBy, Guid subjectId);
    Task<IEnumerable<ActivityDetailModel>> GetActivitiesBySubjectIdAsync(Guid subjectId);
    Task CreateActivityAsync(ActivityDetailModel activityDetailModel);
    Task DeleteActivityAsync(Guid activityId);
}