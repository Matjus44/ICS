using ICS.BL.Facades.Interfaces;
using ICS.BL.Mappers;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;
using ICS.DAL.Mappers;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class RatingFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    IRatingModelMapper modelMapper)
    :
        FacadeBase<RatingEntity, RatingListModel, RatingDetailModel, RatingEntityMapper>(
            unitOfWorkFactory, modelMapper), IRatingFacade
{
    public async Task<IEnumerable<RatingListModel>> FilterRatingsAsync(string filter, object value)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();

        IQueryable<RatingEntity> query = ratingRepository.Get();

        switch (filter.ToLower())
        {
            case "id":
                if (value is Guid idValue)
                {
                    query = query.Where(r => r.Id == idValue);
                }
                break;
            case "activityid":
                if (value is Guid activityIdValue)
                {
                    query = query.Where(r => r.ActivityId == activityIdValue);
                }
                break;
            default:
                throw new ArgumentException("Invalid filter provided.");
        }

        var filteredRatings = await query.ToListAsync();

        return ModelMapper.MapToListModel(filteredRatings);
    }
    
    public async Task<IEnumerable<RatingDetailModel>> GetStudentsRatingsAsync(Guid studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();

        var ratings = await ratingRepository.Get()
            .Include(r => r.Student)
            .Include(r => r.Activity)
            .Where(r => r.StudentId == studentId)
            .ToListAsync();

        return ratings.Select(rating => ModelMapper.MapToDetailModel(rating));
    }

    public async Task<IEnumerable<RatingDetailModel>> GetStudentsRatingsInSubjectAsync(Guid studentId, Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();
        
        var ratings = await ratingRepository.Get()
            .Include(r => r.Student)
            .Include(r => r.Activity)
            .Where(r => r.StudentId == studentId && r.Activity.SubjectId == subjectId)
            .ToListAsync();
        
        return ratings.Select(rating => ModelMapper.MapToDetailModel(rating));
    }

    public Task AddRatingAsync(RatingEntity ratingEntity)
    {
        throw new NotImplementedException();
    }

    public async Task AddRatingAsync(RatingDetailModel ratingEntity)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();
        
        var newRating = ModelMapper.MapToEntity(ratingEntity);
        
        ratingRepository.Insert(newRating);
        await uow.CommitAsync();
    }
   
    
    public async Task<IEnumerable<RatingDetailModel>> SortStudentsRatingsInActivityAsync(string sortBy, Guid studentId, Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();
        
        var ratings = await ratingRepository.Get()
            .Include(r => r.Student)
            .Include(r => r.Activity)
            .Where(r => r.StudentId == studentId && r.Activity.SubjectId == subjectId)
            .ToListAsync();

        switch (sortBy)
        {
            case "Lowest First":
                ratings = ratings.OrderBy(a => a.Rating).ToList();
                break;
            case "Highest First":
                ratings = ratings.OrderByDescending(a => a.Rating).ToList();
                break;
        }
        
        var sortedRatings = ratings.Select(rating => ModelMapper.MapToDetailModel(rating));
        return sortedRatings;
    }
    
}