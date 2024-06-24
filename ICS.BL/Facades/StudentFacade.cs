using ICS.BL.Facades.Interfaces;
using ICS.BL.Mappers.Interfaces;
using ICS.BL.Models;
using ICS.DAL.Entities;
using ICS.DAL.Mappers;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class StudentFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    IStudentModelMapper modelMapper)
    : FacadeBase<StudentEntity, StudentListModel, StudentDetailModel, StudentEntityMapper>(
            unitOfWorkFactory,
            modelMapper),
        IStudentFacade
{
    public async Task<IEnumerable<StudentListModel>> SearchStudentsByTermAsync(string searchTerm)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        // Split the search term into parts (e.g., "FirstName LastName")
        var searchTerms = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Initial query
        var query = studentRepository
            .Get()
            .Include(x => x.RegisteredSubjects)
            .ThenInclude(x => x.Subject)
            .AsQueryable();

        // Add conditions to the query for each search term
        foreach (var term in searchTerms)
        {
            query = query.Where(s =>
                EF.Functions.Like(s.FirstName, $"%{term}%") || EF.Functions.Like(s.LastName, $"%{term}%"));
        }

        // Execute the query
        var searchedStudents = await query.ToListAsync();

        // Map the results to detail models
        var searchedStudentModels = searchedStudents.Select(student => ModelMapper.MapToListModel(student));

        return searchedStudentModels;
    }

    public async Task<StudentDetailModel> SearchStudentByIdAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        // Query for the student with the specified ID
        var student = await studentRepository
            .Get()
            .Include(x => x.RegisteredSubjects)
            .ThenInclude(x => x.Subject)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            throw new ArgumentException($"Student with ID {id} not found.");
        }

        // Map the student entity to a StudentDetailModel
        var studentDetailModel = ModelMapper.MapToDetailModel(student);
        return studentDetailModel;
    }

    public async Task CreateStudentAsync(StudentDetailModel studentDetailModel)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        // Map the detail model to an entity
        var studentEntity = ModelMapper.MapToEntity(studentDetailModel);

        // Add the entity to the repository
        studentRepository.Insert(studentEntity);

        // Save the changes
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<StudentDetailModel>> GetStudentsBySubjectId(Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        // Query for students registered to the subject with the specified ID
        var studentsQuery = studentRepository
            .Get()
            .Include(x => x.RegisteredSubjects)
            .Where(s => s.RegisteredSubjects.Any(rs => rs.SubjectId == subjectId))
            .AsQueryable();

        // Execute the query and load the results into a list
        var students = await studentsQuery.ToListAsync();

        // Map the results to detail models
        var studentModels = students.Select(student => ModelMapper.MapToDetailModel(student));

        return studentModels;
    }

    public async Task UpdateStudentAsync(StudentDetailModel studentDetailModel)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        // Map the detail model to an entity
        var studentEntity = ModelMapper.MapToEntity(studentDetailModel);

        // Update the entity in the repository
        await studentRepository.UpdateAsync(studentEntity);

        // Save the changes
        await uow.CommitAsync();
    }

    public async Task DeleteStudentAsync(Guid studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        // Delete the student entity with the specified ID from the repository
        await studentRepository.DeleteAsync(studentId);

        // Save the changes
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<StudentListModel>> SortStudentsByOrderAsync(string sortOrder)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity, StudentEntityMapper>();

        var students = await studentRepository.Get().ToListAsync();

        switch (sortOrder)
        {
            case "Ascending Name":
                students = students.OrderBy(s => s.FirstName).ToList();
                break;
            case "Descending Name":
                students = students.OrderByDescending(s => s.FirstName).ToList();
                break;
            case "Ascending Last Name":
                students = students.OrderBy(s => s.LastName).ToList();
                break;
            case "Descending Last Name":
                students = students.OrderByDescending(s => s.LastName).ToList();
                break;
        }

        var sortedStudents = students.Select(student => ModelMapper.MapToListModel(student));
        return sortedStudents;
    }

}