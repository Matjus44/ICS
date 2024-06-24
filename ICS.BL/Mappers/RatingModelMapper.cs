using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class RatingModelMapper() 
    : ModelMapperBase<RatingEntity, RatingListModel, RatingDetailModel>, IRatingModelMapper
{
    public override RatingListModel MapToListModel(RatingEntity? entity)
        => entity is null
            ? RatingListModel.Empty
            : new RatingListModel
            {
                Id = entity.Id,
                Note = entity.Note,
                ActivityId = entity.ActivityId,
                Rating = entity.Rating,
                StudentId = entity.StudentId
            };

    public override RatingDetailModel MapToDetailModel(RatingEntity entity)
        => new RatingDetailModel
        {
            Id = entity.Id,
            Note = entity.Note,
            ActivityId = entity.ActivityId,
            Rating = entity.Rating,
            StudentId = entity.StudentId
        };

    public override RatingEntity MapToEntity(RatingDetailModel model)
        => new()
        {
            Id = model.Id,
            Note = model.Note,
            Rating = model.Rating,
            ActivityId = model.ActivityId,
            StudentId = model.StudentId
        };

    public RatingListModel MapToListModel(RatingDetailModel detailModel)
        => new()
        {
            Id = detailModel.Id,
            Note = detailModel.Note,
            Rating = detailModel.Rating,
            ActivityId = detailModel.ActivityId,
            StudentId = detailModel.StudentId
        };

    public override RatingEntity MapToEntity(RatingDetailModel model, Guid id)
        => new()
        {
            Id = id,
            Note = model.Note,
            Rating = model.Rating,
            ActivityId = model.ActivityId,
            StudentId = model.StudentId
        };
}