using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;

public static class RatingSeeds
{
    public static readonly RatingEntity EmptyRatingEntity = new()
    {
        Id = Guid.Empty,
        StudentId = default,
        ActivityId = default,
        Rating = default,
        Note = default,
        Student = default!,
        Activity = default!,
    };

    public static readonly RatingEntity RatingEntity1 = new()
    {
        Id = Guid.NewGuid(),
        StudentId = StudentSeeds.StudentEntity1.Id,
        ActivityId = ActivitySeeds.ActivityEntity1.Id,
        Rating = 5,
        Note = "Test 1",
        Student = StudentSeeds.StudentEntity1,
        Activity = ActivitySeeds.ActivityEntity1,
    };

    public static readonly RatingEntity RatingEntity2 = new()
    {
        Id = Guid.NewGuid(),
        StudentId = StudentSeeds.StudentEntity2.Id,
        ActivityId = ActivitySeeds.ActivityEntity2.Id,
        Rating = 4,
        Note = "Test 2",
        Student = StudentSeeds.StudentEntity2,
        Activity = ActivitySeeds.ActivityEntity2,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly RatingEntity RatingEntityUpdate = RatingEntity1 with
    {
        Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"),
        Student = null!,
        Activity = null!,
        StudentId = StudentSeeds.StudentEntity1.Id,
        ActivityId = ActivitySeeds.ActivityEntity1.Id
    };

    public static readonly RatingEntity RatingEntityDelete = RatingEntity1 with
    {
        Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"),
        Student = null!,
        Activity = null!,
        StudentId = StudentSeeds.StudentEntity2.Id,
        ActivityId = ActivitySeeds.ActivityEntity2.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RatingEntity>().HasData(
            RatingEntity1 with { Student = null!, Activity = null! },
            RatingEntity2 with { Student = null!, Activity = null! },
            RatingEntityUpdate,
            RatingEntityDelete
        );
    }
}
