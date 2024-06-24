using ICS.Common.Enums;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ICS.DAL.Tests;

public class DbContextSubjectTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Subject_Persisted()
    {
        //Arrange
        var entity = SubjectSeeds.EmptySubjectEntity with
        {
            Name = "C# Seminar",
            CodeName = "ICS",
        };

        //Act
        AppDbContextSUT.SubjectEntities.Add(entity);
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx
            .SubjectEntities
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task AddNew_SubjectWithActivities_Persisted()
    {
        //Arrange
        var entity = SubjectSeeds.EmptySubjectEntity with
        {
            Name = "C# Seminar",
            CodeName = "ICS",
            Activities = new List<ActivityEntity>()
            {
                ActivitySeeds.EmptyActivityEntity with
                {
                    Start = DateTime.UtcNow,
                    Finish = DateTime.UtcNow.AddHours(1),
                    Room = RoomName.D105,
                    Type = ActivityType.Exam,
                    ActivityDescription = "Tasting activity 1",
                    Ratings = []
                },
            }
        };

        //Act
        AppDbContextSUT.SubjectEntities.Add(entity);
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx
            .SubjectEntities
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetAll_Subjects_ContainsSeededSubject()
    {
        //Act
        var entities = await AppDbContextSUT.SubjectEntities.ToListAsync();

        //Assert
        DeepAssert.Contains(SubjectSeeds.SubjectEntity1, entities,
            nameof(SubjectEntity.RegisteredStudents), nameof(SubjectEntity.Activities));
    }

    [Fact]
    public async Task GetById_Subject()
    {
        //Act
        var entity = await AppDbContextSUT
            .SubjectEntities
            .SingleAsync(i => i.Id == SubjectSeeds.SubjectEntity1.Id);

        //Assert
        DeepAssert.Equal(SubjectSeeds.SubjectEntity1, entity);
    }

    [Fact]
    public async Task GetById_IncludingActivities_Subject()
    {
        //Act
        var entity = await AppDbContextSUT
            .SubjectEntities
            .Include(i => i.Activities)
            .SingleOrDefaultAsync(i => i.Id == SubjectSeeds.SubjectEntity1.Id);

        //Assert
        DeepAssert.Equal(SubjectSeeds.SubjectEntity1, entity);
    }

    [Fact]
    public async Task Update_Subject_Persisted()
    {
        //Arrange
        var entity1 = SubjectSeeds.EmptySubjectEntity with
        {
            Name = "Test",
            CodeName = "Test"
        };

        AppDbContextSUT.SubjectEntities.Update(entity1);
        await AppDbContextSUT.SaveChangesAsync();

        //Act
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var entity = await dbx
                .SubjectEntities
                .SingleAsync(i => i.Id == entity1.Id);

            entity.Name = "UPDATED";
            await dbx.SaveChangesAsync();
        }

        //Assert
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var updatedEntity = await dbx
                .SubjectEntities
                .SingleAsync(i => i.Id == entity1.Id);
            Assert.Equal("UPDATED", updatedEntity.Name);
        }
    }

    [Fact]
    public async Task Delete_SubjectWithoutActivities_Deleted()
    {
        //Arrange
        var baseEntity = SubjectSeeds.SubjectEntityDelete;

        //Act
        AppDbContextSUT.SubjectEntities.Remove(baseEntity);
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await AppDbContextSUT.SubjectEntities.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_RecipeWithoutIngredients_Deleted()
    {
        //Arrange
        var baseEntity = SubjectSeeds.SubjectEntityDelete;

        //Act
        AppDbContextSUT.Remove(
            AppDbContextSUT.SubjectEntities.Single(i => i.Id == baseEntity.Id));
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await AppDbContextSUT.SubjectEntities.AnyAsync(i => i.Id == baseEntity.Id));
    }
}