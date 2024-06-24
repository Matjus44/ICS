using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Facades.Interfaces;

public interface ISubjectFacade : IFacade<SubjectEntity, SubjectListModel, SubjectDetailModel>
{
    new Task<SubjectDetailModel?> GetAsync(Guid id);

    // Task<IEnumerable<ActivityListModel>> FilterActivitiesBySubjectAndTimeAsync(Guid subjectId, DateTime startTime,
    //     DateTime endTime);
    Task<IEnumerable<SubjectDetailModel>> SortSubjectsByOrderAsync(string sortOrder);
    Task<IEnumerable<SubjectDetailModel>> GetAllSubjectsAsync();
    Task<IEnumerable<ActivityDetailModel>> GetActivitiesForSubject(Guid subjectId);
    Task RegisterStudentToSubject(Guid subjectId, Guid studentId);
    Task UnregisterStudentFromSubject(Guid subjectId, Guid studentId);
    Task<SubjectDetailModel> GetSubjectDetailByIdAsync(Guid subjectId);
    Task<IEnumerable<SubjectDetailModel>> SearchSubjectsByNameAsync(string searchTerm);
    Task<IEnumerable<SubjectListModel>> SearchStudentsSubjectsByNameAsync(string searchTerm, Guid studentId);
    Task CreateSubjectAsync(SubjectDetailModel subjectDetailModel);
    
    Task DeleteSubjectAsync(Guid subjectId);
}