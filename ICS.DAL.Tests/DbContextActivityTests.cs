using ICS.Common.Enums;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using KellermanSoftware.CompareNetObjects;

namespace ICS.DAL.Tests;

public class DbContextActivityTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task CanAddNewActivityToExitingSubject()
    {
        // Arrange
        var existingSubjectId = SubjectSeeds.SubjectEntity1.Id;
        var newActivityId = Guid.NewGuid();

        // Act
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var subject = await dbContext
                .SubjectEntities
                .Include(x => x.Activities)
                .FirstOrDefaultAsync(x => x.Id == existingSubjectId);

            if (subject is null)
            {
                throw new Exception($"Subject with id {existingSubjectId} does not exist!");
            }

            var activity = new ActivityEntity()
            {
                Id = newActivityId,
                Subject = subject,
                Start = DateTime.UtcNow,
                Finish = DateTime.UtcNow.AddHours(1),
                Room = RoomName.D105,
                Type = ActivityType.Exam,
                ActivityDescription = "Tasting activity 1"
            };

            dbContext.Add(activity);

            await dbContext.SaveChangesAsync();
        }

        // Assert
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var subject = await dbContext
                .SubjectEntities
                .Include(x => x.Activities)
                .FirstAsync(x => x.Id == existingSubjectId);

            Assert.Contains(subject.Activities, (activity) => activity.Id == newActivityId);
        }
    }
    [Fact]
    public async Task DeleteActivityById()
    {
        // Arrange
        var newActivityId = Guid.NewGuid(); 
        var existingSubjectId = SubjectSeeds.SubjectEntity1.Id;

        
        await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
        {
            var newActivity = new ActivityEntity
            {
                Id = newActivityId,
                SubjectId = existingSubjectId,
                Start = DateTime.UtcNow,
                Finish = DateTime.UtcNow.AddHours(1),
                Room = RoomName.D105,
                Type = ActivityType.Exam,
                ActivityDescription = "Activity to be deleted"
            };

            setupContext.ActivityEntities.Add(newActivity);
            await setupContext.SaveChangesAsync();
        }

        // Act
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var activityToDelete = await dbContext.ActivityEntities.FindAsync(newActivityId);
            if (activityToDelete != null)
            {
                dbContext.ActivityEntities.Remove(activityToDelete);
                await dbContext.SaveChangesAsync();
            }
        }

        // Assert
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var deletedActivity = await dbContext.ActivityEntities.FindAsync(newActivityId);
            Assert.Null(deletedActivity); // Pokud je aktivita smazána, nenajdeme ji
        }
    }
    
    [Fact]
    public async Task Update_Activity_Persisted()
{
    // Arrange
    var existingActivityId = Guid.NewGuid();
    var existingSubjectId = SubjectSeeds.SubjectEntity1.Id;

    
    await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
    {
        setupContext.ActivityEntities.Add(new ActivityEntity
        {
            Id = existingActivityId,
            SubjectId = existingSubjectId,
            Start = DateTime.UtcNow.AddDays(-1),
            Finish = DateTime.UtcNow.AddHours(2), 
            Room = RoomName.D105,
            Type = ActivityType.Exam,
            ActivityDescription = "Original Activity"
        });
        await setupContext.SaveChangesAsync();
    }

    // Act
    await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
    {
        var activityToUpdate = await dbContext.ActivityEntities.FindAsync(existingActivityId);
        if (activityToUpdate != null)
        {
            activityToUpdate.Start = DateTime.UtcNow; 
            activityToUpdate.Finish = DateTime.UtcNow.AddHours(3); 
            activityToUpdate.Room = RoomName.D0206; 
            activityToUpdate.Type = ActivityType.Lecture; 
            activityToUpdate.ActivityDescription = "Updated Activity"; 
            await dbContext.SaveChangesAsync();
        }
    }

    // Assert 
    await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
    {
        var updatedActivity = await dbContext.ActivityEntities.FindAsync(existingActivityId);
        Assert.NotNull(updatedActivity);
        Assert.Equal(DateTime.UtcNow.Date, updatedActivity.Start.Date); 
        Assert.Equal("Updated Activity", updatedActivity.ActivityDescription);
        Assert.Equal(RoomName.D0206, updatedActivity.Room);
        Assert.Equal(ActivityType.Lecture, updatedActivity.Type);
        
    }
}
    [Fact]
    public async Task GetById_Activity()
    {
        // Arrange
        var newActivityId = Guid.NewGuid();
        var existingSubjectId = SubjectSeeds.SubjectEntity1.Id;

        
        await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
        {
            setupContext.ActivityEntities.Add(new ActivityEntity
            {
                Id = newActivityId,
                SubjectId = existingSubjectId,
                Start = DateTime.UtcNow,
                Finish = DateTime.UtcNow.AddHours(1),
                Room = RoomName.D105,
                Type = ActivityType.Exam,
                ActivityDescription = "Activity for GetById test"
            });
            await setupContext.SaveChangesAsync();
        }

        // Act
        ActivityEntity? activityEntity;
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            activityEntity = await dbContext.ActivityEntities.FindAsync(newActivityId);
        }

        // Assert
        Assert.NotNull(activityEntity);
        Assert.Equal(newActivityId, activityEntity.Id);
        Assert.Equal("Activity for GetById test", activityEntity.ActivityDescription);
        
    }

    [Fact]
    public async Task GetById_Activity_IncludingSubject()
    {
        // Arrange
        var newActivityId = Guid.NewGuid();
        var existingSubjectId = SubjectSeeds.SubjectEntity1.Id;
        
        await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
        {
            var activity = new ActivityEntity
            {
                Id = newActivityId,
                SubjectId = existingSubjectId,
                Start = DateTime.UtcNow,
                Finish = DateTime.UtcNow.AddHours(1),
                Room = RoomName.D105,
                Type = ActivityType.Exam,
                ActivityDescription = "Activity with Subject for GetById test"
            };
            setupContext.ActivityEntities.Add(activity);
            await setupContext.SaveChangesAsync();
        }

        // Act
        ActivityEntity? activityEntity;
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            activityEntity = await dbContext.ActivityEntities
                .Include(a => a.Subject)
                .SingleOrDefaultAsync(a => a.Id == newActivityId);
        }

        // Assert
        Assert.NotNull(activityEntity);
        Assert.Equal(newActivityId, activityEntity.Id);
        Assert.NotNull(activityEntity.Subject);
        Assert.Equal(existingSubjectId, activityEntity.Subject.Id);
        
    }

}