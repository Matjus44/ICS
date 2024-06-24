using ICS.DAL;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity EmptyStudentEntity = new()
    {
        Id = Guid.Empty,
        FirstName = default,
        LastName = default,
        PhotoUri = default,
        RegisteredSubjects = new List<StudentsSubjectsEntity>()
    };

    public static readonly StudentEntity StudentEntity1 = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "John",
        LastName = "Doe",
        PhotoUri = new Uri("https://example.com/photo1.jpg"),
        RegisteredSubjects = new List<StudentsSubjectsEntity>()
    };

    public static readonly StudentEntity StudentEntity2 = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "Jane",
        LastName = "Smith",
        PhotoUri = new Uri("https://example.com/photo2.jpg"),
        RegisteredSubjects = new List<StudentsSubjectsEntity>()
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly StudentEntity StudentEntityUpdate = StudentEntity1 with
    {
        Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"),
        FirstName = "UpdatedFirstName",
        LastName = "UpdatedLastName",
        PhotoUri = new Uri("https://example.com/photo_updated.jpg")
    };

    public static readonly StudentEntity StudentEntityDelete = StudentEntity1 with
    {
        Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279")
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            StudentEntity1,
            StudentEntity2,
            StudentEntityUpdate,
            StudentEntityDelete
        );
    }
    
    public static void Seed(AppDbContext dbContext)
    {
        dbContext.Add(StudentEntity1);
        dbContext.Add(StudentEntity2);
        dbContext.Add(StudentEntityUpdate);
        dbContext.Add(StudentEntityDelete);
    }
}
