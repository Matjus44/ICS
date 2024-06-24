using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;


namespace ICS.DAL.Tests;

public class DbContextRatingTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task CanAddNewRatingToExistingActivity()
    {
        // Arrange
        var existingActivityId = SubjectSeeds.SubjectEntity1.Activities.First().Id;
        var existingStudentId = StudentSeeds.StudentEntity1.Id;
        var newRatingId = Guid.NewGuid();

        // Act
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var activity = await dbContext
                .ActivityEntities
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.Id == existingActivityId);

            if (activity is null)
            {
                throw new Exception($"Activity with id {existingActivityId} does not exist!");
            }

            var rating = new RatingEntity()
            {
                Id = newRatingId,
                Note = "Super",
                Rating = 100,
                Activity = activity,
                StudentId = existingStudentId
            };

            dbContext.Add(rating);
            
            await dbContext.SaveChangesAsync();
        }

        // Assert
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var activity = await dbContext
                .ActivityEntities
                .Include(x => x.Ratings)
                .FirstAsync(x => x.Id == existingActivityId);
        
            Assert.Contains(activity.Ratings, (rating) => rating.Id == newRatingId);
        }
        
    }



    
    [Fact]
    public async Task GetById_Only_Rating()
    {
        // Arrange
        var newRatingId = Guid.NewGuid(); 
        var existingStudentId = StudentSeeds.StudentEntity1.Id; 
        var existingActivityId = SubjectSeeds.SubjectEntity1.Activities.First().Id; 
        
        await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
        {
            var activity = await setupContext.ActivityEntities.FindAsync(existingActivityId);
            if (activity == null)
            {
                throw new Exception($"Activity with id {existingActivityId} does not exist!");
            }

            var newRating = new RatingEntity
            {
                Id = newRatingId,
                Note = "Excellent!",
                Rating = 5,
                StudentId = existingStudentId,
                ActivityId = existingActivityId 
            };

            setupContext.RatingEntities.Add(newRating);
            await setupContext.SaveChangesAsync();
        }

        // Act 
        RatingEntity ?retrievedRating = null;
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
                retrievedRating = await dbContext.RatingEntities
                .Include(r => r.Activity) 
                .Include(r => r.Student) 
                .FirstOrDefaultAsync(r => r.Id == newRatingId);
        } 
        // Assert
        Assert.NotNull(retrievedRating);
        Assert.Equal("Excellent!", retrievedRating.Note);
        Assert.Equal(5, retrievedRating.Rating);
        Assert.Equal(existingStudentId, retrievedRating.StudentId);
        Assert.Equal(existingActivityId, retrievedRating.ActivityId);
    }
    
    
    [Fact]
    public async Task DeleteRatingById()
    {
        // Arrange
        
        var newRatingId = Guid.NewGuid(); 
        var existingActivityId = SubjectSeeds.SubjectEntity1.Activities.First().Id;
        var existingStudentId = StudentSeeds.StudentEntity1.Id;

        await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
        {
            setupContext.RatingEntities.Add(new RatingEntity
            {
                Id = newRatingId,
                Rating = 5,
                Note = "Rating to be deleted",
                ActivityId = existingActivityId,
                StudentId = existingStudentId
            });
            await setupContext.SaveChangesAsync();
        }

        // Act
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var ratingToDelete = await dbContext.RatingEntities.FindAsync(newRatingId);
            if (ratingToDelete != null)
            {
                dbContext.RatingEntities.Remove(ratingToDelete);
                await dbContext.SaveChangesAsync();
            }
        }

        // Assert
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var deletedRating = await dbContext.RatingEntities.FindAsync(newRatingId);
            Assert.Null(deletedRating); 
        }
    }

    [Fact]
    public async Task Update_Rating_Persisted()
    {
        // Arrange
        var existingRatingId = Guid.NewGuid();
        var existingActivityId = SubjectSeeds.SubjectEntity1.Activities.First().Id;
        var existingStudentId = StudentSeeds.StudentEntity1.Id;

        
        await using (var setupContext = await DbContextFactory.CreateDbContextAsync())
        {
            setupContext.RatingEntities.Add(new RatingEntity
            {
                Id = existingRatingId,
                Rating = 1, 
                Note = "Original Note",
                ActivityId = existingActivityId,
                StudentId = existingStudentId
            });
            await setupContext.SaveChangesAsync();
        }

        // Act
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var ratingToUpdate = await dbContext.RatingEntities.FindAsync(existingRatingId);
            if (ratingToUpdate != null)
            {
                ratingToUpdate.Rating = 5; 
                ratingToUpdate.Note = "Updated Note";
                await dbContext.SaveChangesAsync();
            }
        }

        // Assert
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            var updatedRating = await dbContext.RatingEntities.FindAsync(existingRatingId);
            Assert.NotNull(updatedRating);
            Assert.Equal(5, updatedRating.Rating);
            Assert.Equal("Updated Note", updatedRating.Note);
        }
    }
    
    [Fact]
    public async Task GetAll_Ratings_ForActivity()
    {
        // Arrange
        var existingActivityId = SubjectSeeds.SubjectEntity1.Activities.First().Id;
        var existingStudentId = StudentSeeds.StudentEntity1.Id;
        
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            dbContext.RatingEntities.Add(new RatingEntity
            {
                Id = Guid.NewGuid(),
                Rating = 1, 
                Note = "Note 1",
                ActivityId = existingActivityId,
                StudentId = existingStudentId
            });
            dbContext.RatingEntities.Add(new RatingEntity
            {
                Id = Guid.NewGuid(),
                Rating = 2, 
                Note = "Note 2",
                ActivityId = existingActivityId,
                StudentId = existingStudentId
            });
            dbContext.RatingEntities.Add(new RatingEntity
            {
                Id = Guid.NewGuid(),
                Rating = 3, 
                Note = "Note 3",
                ActivityId = existingActivityId,
                StudentId = existingStudentId
            });
            await dbContext.SaveChangesAsync();
        }

        // Act
        List<RatingEntity> ratings = null;
        await using (var dbContext = await DbContextFactory.CreateDbContextAsync())
        {
            ratings = await dbContext.RatingEntities
                .Where(r => r.ActivityId == existingActivityId)
                .ToListAsync();
        }

        // Assert
        Assert.NotNull(ratings);
        Assert.Equal(3, ratings.Count);
    }


}

