using System.Collections.ObjectModel;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class StudentModelMapper : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailModel>, IStudentModelMapper
{
    public override StudentListModel MapToListModel(StudentEntity? entity)
        => entity is null
            ? StudentListModel.Empty
            : new StudentListModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUri = entity.PhotoUri,
                RegisteredSubjects = entity.RegisteredSubjects
                    .Select(s => new SubjectListModel
                    {
                        Id = s.Subject.Id,
                        Name = s.Subject.Name,
                        CodeName = s.Subject.CodeName
                    })
                    .ToObservableCollection()
            };
    
    public override StudentDetailModel MapToDetailModel(StudentEntity entity)
    {
        return new StudentDetailModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhotoUri = entity.PhotoUri,
            RegisteredSubjects = entity.RegisteredSubjects == null
                ? new ObservableCollection<SubjectListModel>()
                : entity.RegisteredSubjects
                    .Where(s => s.Subject != null)
                    .Select(s => new SubjectListModel
                    {
                        Id = s.Subject.Id,
                        Name = s.Subject.Name,
                        CodeName = s.Subject.CodeName
                    })
                    .ToObservableCollection()
        };
    }
    
    public override StudentEntity MapToEntity(StudentDetailModel model)
        => new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhotoUri = model.PhotoUri
        };

    public StudentListModel MapToListModel(StudentDetailModel detailModel)
        => new()
        {
            Id = detailModel.Id,
            FirstName = detailModel.FirstName,
            LastName = detailModel.LastName,
            PhotoUri = detailModel.PhotoUri,
            RegisteredSubjects = detailModel.RegisteredSubjects
            
        };
    
    public override StudentEntity MapToEntity(StudentDetailModel model, Guid id)
        => new()
        {
            Id = id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhotoUri = model.PhotoUri
        };

    public StudentEntity MapToEntity(StudentListModel model)
          => new()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhotoUri = null
            };
}