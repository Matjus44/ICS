using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers.Interfaces;

public interface IStudentModelMapper
    : IModelMapper<StudentEntity, StudentListModel, StudentDetailModel>
{
    StudentListModel MapToListModel(StudentDetailModel detailModel);
    StudentEntity MapToEntity(StudentListModel model);
}