using ICS.Common.Enums;
using ICS.DAL;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity EmptySubjectEntity = new()
    {
        Id = Guid.Empty,
        Name = default,
        CodeName = default,
        RegisteredStudents = [],
        Activities = []
    };

    public static readonly SubjectEntity SubjectEntity1 = new()
    {
        Id = Guid.NewGuid(),
        Name = "C# Seminar",
        CodeName = "ICS",
        Activities =
        [
            new ActivityEntity()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.UtcNow,
                Finish = DateTime.UtcNow.AddHours(1),
                Room = RoomName.D105,
                Type = ActivityType.Exam,
                ActivityDescription = "Tasting activity 1",
                Ratings = []
            }
        ]
    };

    public static readonly SubjectEntity SubjectEntity2 = new()
    {
        Id = Guid.NewGuid(),
        Name = "Mathematics 1",
        CodeName = "IMA1",
        RegisteredStudents = [],
        Activities = []
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly SubjectEntity SubjectEntityUpdate = SubjectEntity1 with
    {
        Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"),
        Name = "C# seminar updated",
        CodeName = "ICS updated",
        Activities = []
    };

    public static readonly SubjectEntity SubjectEntityDelete = SubjectEntity1 with
    {
        Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"),
        Activities = []
    };
    
    public static void Seed(AppDbContext dbContext)
    {
        dbContext.Add(SubjectEntity1);
        dbContext.Add(SubjectEntity2);
        dbContext.Add(SubjectEntityUpdate);
        dbContext.Add(SubjectEntityDelete);
    }
    
    // public static readonly SubjectEntity SubjectEntity = new()
    // {
    //     Name = "OS",
    //     CodeName = "IOS",
    //     Activities =
    //     [
    //         new ActivityEntity()
    //         {
    //             Start = DateTime.UtcNow,
    //             Finish = DateTime.UtcNow.AddHours(1),
    //             Room = Room.D105,
    //             Type = ActivityType.Exam,
    //             ActivityDescription = "Tasting activity 1",
    //             Ratings = []
    //         }
    //     ]
    // };
}