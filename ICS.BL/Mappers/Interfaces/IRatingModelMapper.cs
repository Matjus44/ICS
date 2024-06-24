using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers.Interfaces;

public interface IRatingModelMapper
    : IModelMapper<RatingEntity, RatingListModel, RatingDetailModel>
{
    RatingListModel MapToListModel(RatingDetailModel detailModel);
}