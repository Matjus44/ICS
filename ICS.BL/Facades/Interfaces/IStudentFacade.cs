using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Facades.Interfaces;

public interface IStudentFacade : IFacade<StudentEntity, StudentListModel, StudentDetailModel>
{
    Task<IEnumerable<StudentListModel>> SearchStudentsByTermAsync(string searchTerm);
    Task<StudentDetailModel> SearchStudentByIdAsync(Guid id);
    Task CreateStudentAsync(StudentDetailModel studentDetailModel);
    Task<IEnumerable<StudentDetailModel>> GetStudentsBySubjectId(Guid subjectId);
    Task UpdateStudentAsync(StudentDetailModel studentDetailModel);
    Task DeleteStudentAsync(Guid studentId);
    
    Task<IEnumerable<StudentListModel>> SortStudentsByOrderAsync(string sortOrder);
}