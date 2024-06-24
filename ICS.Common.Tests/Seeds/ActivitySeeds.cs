using ICS.Common.Enums;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ICS.DAL;

namespace ICS.Common.Tests.Seeds
{
    public static class ActivitySeeds
    {
        public static readonly ActivityEntity EmptyActivityEntity = new()
        {
            Id = Guid.Empty,
            SubjectId = default,
            Start = default,
            Finish = default,
            Room = default,
            Type = default,
            ActivityDescription = default,
            Ratings = new List<RatingEntity>(),
            Subject = null
        };

        public static readonly ActivityEntity ActivityEntity1 = new()
        {
            Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"),
            Start = DateTime.UtcNow,
            Finish = DateTime.UtcNow.AddHours(1),
            Room = RoomName.D105,
            Type = ActivityType.Exam,
            ActivityDescription = "Tasting activity 1",
            Ratings = new List<RatingEntity>(),
        };

        public static readonly ActivityEntity ActivityEntity2 = new()
        {
            Id = Guid.Parse("B2E6849D-A158-4436-980C-7FC26B60C674"),
            SubjectId = SubjectSeeds.SubjectEntity2.Id,
            Start = DateTime.UtcNow,
            Finish = DateTime.UtcNow.AddHours(1),
            Room = RoomName.D0206,
            Type = ActivityType.Lecture,
            ActivityDescription = "Tasting activity 2",
            Ratings = new List<RatingEntity>(),
        };

        //To ensure that no tests reuse these clones for non-idempotent operations
        public static readonly ActivityEntity ActivityEntityUpdate = ActivityEntity1 with
        {
            Id = Guid.Parse("C2E6849D-A158-4436-980C-7FC26B60C674"),
            SubjectId = SubjectSeeds.SubjectEntityUpdate.Id
        };

        public static readonly ActivityEntity ActivityEntityDelete = ActivityEntity1 with
        {
            Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"),
            SubjectId = SubjectSeeds.SubjectEntityDelete.Id
        };

        // public static void Seed(this ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<ActivityEntity>().HasData(
        //         ActivityEntity1,
        //         ActivityEntity2,
        //         ActivityEntityUpdate,
        //         ActivityEntityDelete
        //     );
        // }
        
        public static void Seed(AppDbContext dbContext)
        {
            dbContext.Add(ActivityEntity1);
            dbContext.Add(ActivityEntity2);
            dbContext.Add(ActivityEntityUpdate);
            dbContext.Add(ActivityEntityDelete);
        }
    }
}


