using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers.Interfaces;

public interface IStudentsSubjectsModelMapper 
    : IModelMapper<StudentsSubjectsEntity, StudentsSubjectsListModel, StudentsSubjectsDetailModel>
{
    StudentsSubjectsListModel MapToListModel(StudentsSubjectsDetailModel detailModel);
}