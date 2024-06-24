using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class StudentsSubjectsModelMapper(IStudentModelMapper studentModelMapper, ISubjectModelMapper subjectModelMapper) 
    : IModelMapper<StudentsSubjectsEntity, StudentsSubjectsListModel, StudentsSubjectsDetailModel>, IStudentsSubjectsModelMapper
{
    public StudentsSubjectsListModel MapToListModel(StudentsSubjectsEntity? entity)
        => entity is null
            ? StudentsSubjectsListModel.Empty
            : new StudentsSubjectsListModel
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                SubjectId = entity.SubjectId
            };

    public StudentsSubjectsDetailModel MapToDetailModel(StudentsSubjectsEntity entity)
        => new StudentsSubjectsDetailModel
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            SubjectId = entity.SubjectId,
            Student = studentModelMapper.MapToListModel(entity.Student),
            Subject = subjectModelMapper.MapToListModel(entity.Subject)
        };

    public StudentsSubjectsEntity MapToEntity(StudentsSubjectsDetailModel model)
        => new()
        {
            Id = model.Id,
            StudentId = model.StudentId,
            SubjectId = model.SubjectId
        };

    public StudentsSubjectsListModel MapToListModel(StudentsSubjectsDetailModel detailModel)
        => new()
        {
            Id = detailModel.Id,
            StudentId = detailModel.StudentId,
            SubjectId = detailModel.SubjectId
        };

    public StudentsSubjectsEntity MapToEntity(StudentsSubjectsDetailModel model, Guid id)
        => new()
        {
            Id = id,
            StudentId = model.StudentId,
            SubjectId = model.SubjectId
        };
}