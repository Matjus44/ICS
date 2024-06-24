using ICS.BL.Facades.Interfaces;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;
using ICS.DAL.Mappers;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class ActivityFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    IActivityModelMapper modelMapper)
    :
        FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>(
            unitOfWorkFactory, modelMapper), IActivityFacade
{
    public async Task<IEnumerable<ActivityDetailModel>> SortActivitiesAsync(string sortBy, Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var activityRepository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
    
        
        var activities = await activityRepository.Get()
            .Where(a => a.SubjectId == subjectId)
            .ToListAsync();
    
        
        switch (sortBy)
        {
            case "Ascending":
                activities = activities.OrderBy(a => a.ActivityDescription).ToList();
                break;
            case "Descending":
                activities = activities.OrderByDescending(a => a.ActivityDescription).ToList();
                break;
            case "StartTime":
                activities = activities.OrderBy(a => a.Start).ToList();
                break;
            case "FinishTime":
                activities = activities.OrderBy(a => a.Finish).ToList();
                break;
            case "Room":
                activities = activities.OrderBy(a => a.Room).ToList();
                break;
            case "SubjectCode":
                activities = activities.OrderBy(a => a.Subject.CodeName).ToList();
                break;
            default:
                Console.WriteLine("No activity found with these filters");
                break;
        }
    
        // Mapovanie zotriedených aktivít na modely
        var sortedActivityModels = activities.Select(activity => ModelMapper.MapToDetailModel(activity));
    
        return sortedActivityModels;
    }
    
    public async Task<IEnumerable<ActivityDetailModel>> GetActivitiesBySubjectIdAsync(Guid subjectId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var activityRepository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();;

        // Načítanie aktivít, ktoré zodpovedajú zadanému SubjectId
        var activities = await activityRepository
            .Get()
            .Where(a => a.SubjectId == subjectId)  // Pridanie podmienky na filtrovanie podľa SubjectId
            .ToListAsync();

        // Mapovanie načítaných entít na ActivityDetailModel pomocou ModelMapper
        var sortedActivityModels = activities.Select(activity => ModelMapper.MapToDetailModel(activity));
    
        return sortedActivityModels;
    }
    
    // New method to create an activity
    public async Task CreateActivityAsync(ActivityDetailModel activityDetailModel)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var activityRepository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        var newActivity = ModelMapper.MapToEntity(activityDetailModel);

        activityRepository.Insert(newActivity);
        await uow.CommitAsync();
    }
    
    
    public async Task DeleteActivityAsync(Guid activityId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var activityRepository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();
        var activity = await activityRepository.Get()
            .Include(a => a.Ratings)
            .SingleOrDefaultAsync(a => a.Id == activityId);

        if (activity != null)
            foreach (var ratingEntity in activity.Ratings.ToList())
            {
                await ratingRepository.DeleteAsync(ratingEntity.Id);
            }

        await activityRepository.DeleteAsync(activityId);
        await uow.CommitAsync();
    }
}