using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Facades.Interfaces;

public interface IRatingFacade : IFacade<RatingEntity, RatingListModel, RatingDetailModel>
{
    Task<IEnumerable<RatingListModel>> FilterRatingsAsync(string filter, object value);
    Task<IEnumerable<RatingDetailModel>> GetStudentsRatingsAsync(Guid studentId);
    Task<IEnumerable<RatingDetailModel>> GetStudentsRatingsInSubjectAsync(Guid studentId, Guid subjectId);
    
    Task<IEnumerable<RatingDetailModel>> SortStudentsRatingsInActivityAsync(string sortBy, Guid studentId, Guid subjectId);
    Task AddRatingAsync(RatingDetailModel ratingEntity);
}