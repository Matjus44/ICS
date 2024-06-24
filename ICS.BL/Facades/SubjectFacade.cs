using ICS.BL.Facades.Interfaces;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;
using ICS.DAL.Mappers;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class SubjectFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    ISubjectModelMapper modelMapper,
    IActivityFacade activityFacade)
    :
        FacadeBase<SubjectEntity, SubjectListModel, SubjectDetailModel, SubjectEntityMapper>(
            unitOfWorkFactory, modelMapper), ISubjectFacade
{

    public override async Task<SubjectDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        SubjectEntity? entity = await uow
            .GetRepository<SubjectEntity, SubjectEntityMapper>()
            .Get()
            .Include(x => x.Activities)
            .ThenInclude(x => x.Ratings)
            .SingleOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }
    
    // Nová metóda na získanie všetkých predmetov
    public async Task<IEnumerable<SubjectDetailModel>> GetAllSubjectsAsync()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity,SubjectEntityMapper>();

        var subjects = await subjectRepository.Get().ToListAsync();

        // Mapovanie entít na modely
        var subjectModels = subjects.Select(subject => ModelMapper.MapToDetailModel(subject));

        return subjectModels;
    }
    
    public async Task<IEnumerable<ActivityDetailModel>> GetActivitiesForSubject(Guid subjectId)
    {
        return await activityFacade.GetActivitiesBySubjectIdAsync(subjectId);
    }
    
    public async Task RegisterStudentToSubject(Guid subjectId, Guid studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();

        var subject = await subjectRepository.Get()
            .Include(s => s.RegisteredStudents)
            .SingleOrDefaultAsync(s => s.Id == subjectId);

        if (subject == null)
        {
            throw new ArgumentException("Subject not found.", nameof(subjectId));
        }

        if (subject.RegisteredStudents.Any(x => x.StudentId == studentId))
        {
            throw new Exception("Student already registered in this subject");
        }

        var studentsSubjectsEntity = new StudentsSubjectsEntity
        {
            SubjectId = subjectId,
            StudentId = studentId
        };

        subject.RegisteredStudents.Add(studentsSubjectsEntity);

        await uow.CommitAsync();
    }
    
    public async Task UnregisterStudentFromSubject(Guid subjectId, Guid studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();

        var subject = await subjectRepository.Get()
            .Include(s => s.RegisteredStudents)
            .SingleOrDefaultAsync(s => s.Id == subjectId);

        if (subject == null)
        {
            throw new ArgumentException("Subject not found.", nameof(subjectId));
        }

        var studentsSubjectsEntity = subject.RegisteredStudents.SingleOrDefault(x => x.StudentId == studentId);

        if (studentsSubjectsEntity == null)
        {
            throw new ArgumentException("Student not registered in this subject.", nameof(studentId));
        }

        subject.RegisteredStudents.Remove(studentsSubjectsEntity);

        await uow.CommitAsync();
    }

    public async Task<SubjectDetailModel> GetSubjectDetailByIdAsync(Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();

        var subject = await subjectRepository.Get()
            .Include(s => s.Activities)
            .ThenInclude(a => a.Ratings)
            .Include(s => s.RegisteredStudents)
            .SingleOrDefaultAsync(s => s.Id == subjectId);

        if (subject == null)
        {
            throw new ArgumentException("Subject not found.", nameof(subjectId));
        }

        return ModelMapper.MapToDetailModel(subject);
    }
    
    public async Task CreateSubjectAsync(SubjectDetailModel subjectDetailModel)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();

        var newSubject = ModelMapper.MapToEntity(subjectDetailModel);

        subjectRepository.Insert(newSubject);
        await uow.CommitAsync();
    }
    
    public async Task<IEnumerable<SubjectDetailModel>> SearchSubjectsByNameAsync(string searchTerm)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();

        // Split the search term into parts
        var searchTerms = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Initial query
        var query = subjectRepository
            .Get()
            .AsQueryable();

        // Add conditions to the query for each search term
        foreach (var term in searchTerms)
        {
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{term}%"));
        }

        // Execute the query
        var searchedSubjects = await query.ToListAsync();

        // Map the results to detail models
        var searchedSubjectModels = searchedSubjects.Select(subject => ModelMapper.MapToDetailModel(subject));

        return searchedSubjectModels;
    }

    
    public async Task<IEnumerable<SubjectListModel>> SearchStudentsSubjectsByNameAsync(string searchTerm, Guid studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();

        // Split the search term into parts
        var searchTerms = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Initial query
        var query = subjectRepository
            .Get()
            .Include(s => s.RegisteredStudents)
            .AsQueryable();

        // Add conditions to the query for each search term
        foreach (var term in searchTerms)
        {
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{term}%") && s.RegisteredStudents.Any(rs => rs.StudentId == studentId));
        }

        // Execute the query
        var searchedSubjects = await query.ToListAsync();

        // Map the results to detail models
        var searchedSubjectModels = searchedSubjects.Select(subject => ModelMapper.MapToListModel(subject));

        return searchedSubjectModels;
    }

    public async Task<IEnumerable<SubjectDetailModel>> SortSubjectsByOrderAsync(string sortOrder)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();
        var subjects = await subjectRepository.Get().ToListAsync();
        
        switch(sortOrder)
        {
            case "Ascending":
                subjects = await subjectRepository.Get().OrderBy(s => s.Name).ToListAsync();
                break;
            case "Descending":
                subjects = await subjectRepository.Get().OrderByDescending(s => s.Name).ToListAsync();
                break;
            case "CodeName Ascending":
                subjects = await subjectRepository.Get().OrderBy(s => s.CodeName).ToListAsync();
                break;
            case "CodeName Descending":
                subjects = await subjectRepository.Get().OrderByDescending(s => s.CodeName).ToListAsync();
                break;
        }

        var sortedSubjects = subjects.Select(subject => ModelMapper.MapToDetailModel(subject));
        return sortedSubjects;
    }
    
    public async Task DeleteSubjectAsync(Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity, SubjectEntityMapper>();
        var studentsSubjectsRepository = uow.GetRepository<StudentsSubjectsEntity, StudentsSubjectsEntityMapper>();
        var activityRepository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        var ratingRepository = uow.GetRepository<RatingEntity, RatingEntityMapper>();
        var subject = await subjectRepository.Get()
            .Include(s => s.RegisteredStudents)
            .Include(s => s.Activities)
            .ThenInclude(a => a.Ratings)
            .SingleOrDefaultAsync(s => s.Id == subjectId);

        if (subject == null)
        {
            throw new ArgumentException("Subject not found.", nameof(subjectId));
        }
        
        foreach (var studentsSubjectsEntity in subject.RegisteredStudents.ToList())
        {
            await studentsSubjectsRepository.DeleteAsync(studentsSubjectsEntity.Id);
        }
        
        foreach (var activityEntity in subject.Activities.ToList())
        {
            foreach (var ratingEntity in activityEntity.Ratings.ToList())
            {
                await ratingRepository.DeleteAsync(ratingEntity.Id);
            }
            await activityRepository.DeleteAsync(activityEntity.Id);
        }
        
        await subjectRepository.DeleteAsync(subjectId);
        await uow.CommitAsync();
    }
}