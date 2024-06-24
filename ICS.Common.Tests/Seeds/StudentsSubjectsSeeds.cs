using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;

public static class StudentsSubjectsSeeds
{
    public static readonly StudentsSubjectsEntity EmptyStudentsSubjectsEntity = new()
    {
        Id = Guid.Empty,
        StudentId = default,
        SubjectId = default,
        Student = default!,
        Subject = default!,
    };

    public static readonly StudentsSubjectsEntity StudentsSubjectsEntity1 = new()
    {
        Id = Guid.NewGuid(),
        StudentId = StudentSeeds.StudentEntity1.Id,
        SubjectId = SubjectSeeds.SubjectEntity1.Id,
        Student = StudentSeeds.StudentEntity1,
        Subject = SubjectSeeds.SubjectEntity1,
    };

    public static readonly StudentsSubjectsEntity StudentsSubjectsEntity2 = new()
    {
        Id = Guid.NewGuid(),
        StudentId = StudentSeeds.StudentEntity2.Id,
        SubjectId = SubjectSeeds.SubjectEntity2.Id,
        Student = StudentSeeds.StudentEntity2,
        Subject = SubjectSeeds.SubjectEntity2,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly StudentsSubjectsEntity StudentsSubjectsEntityUpdate = StudentsSubjectsEntity1 with
    {
        Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"),
        Student = null!,
        Subject = null!,
        StudentId = StudentSeeds.StudentEntity1.Id,
        SubjectId = SubjectSeeds.SubjectEntity1.Id
    };

    public static readonly StudentsSubjectsEntity StudentsSubjectsEntityDelete = StudentsSubjectsEntity1 with
    {
        Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"),
        Student = null!,
        Subject = null!,
        StudentId = StudentSeeds.StudentEntity1.Id,
        SubjectId = SubjectSeeds.SubjectEntity1.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentsSubjectsEntity>().HasData(
            StudentsSubjectsEntity1 with { Student = null!, Subject = null! },
            StudentsSubjectsEntity2 with { Student = null!, Subject = null! },
            StudentsSubjectsEntityUpdate,
            StudentsSubjectsEntityDelete
        );
    }
}
