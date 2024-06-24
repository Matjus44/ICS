using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class SubjectModelMapper (IActivityModelMapper activityModelMapper) 
    : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailModel>, ISubjectModelMapper
{
    public override SubjectListModel MapToListModel(SubjectEntity? entity)
        => entity is null
            ? SubjectListModel.Empty
            : new SubjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CodeName = entity.CodeName
            };

    public override SubjectDetailModel MapToDetailModel(SubjectEntity entity)
        => new SubjectDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            CodeName = entity.CodeName,
            Activities = activityModelMapper.MapToListModelCollection(entity.Activities)
        };
    
    public override SubjectEntity MapToEntity(SubjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            CodeName = model.CodeName,
            Activities = activityModelMapper.MapToEntityCollection(model.Activities)
        };

    public SubjectListModel MapToListModel(SubjectDetailModel detailModel)
        => new()
        {
            Id = detailModel.Id,
            Name = detailModel.Name,
            CodeName = detailModel.CodeName
        };

    public override SubjectEntity MapToEntity(SubjectDetailModel model, Guid id)
        => new()
        {
            Id = id,
            Name = model.Name,
            CodeName = model.CodeName,
            Activities = activityModelMapper.MapToEntityCollection(model.Activities)
        };
}