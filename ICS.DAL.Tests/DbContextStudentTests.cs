using ICS.DAL.Entities;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ICS.DAL.Tests;

public class DbContextStudentTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Student_Persisted()
    {
        //Arrange
        StudentEntity entity = StudentSeeds.EmptyStudentEntity with
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhotoUri = new Uri("https://example.com/photo.jpg"),
            RegisteredSubjects = []
        };

        //Act
        AppDbContextSUT.StudentEntities.Add(entity);
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.StudentEntities.SingleAsync(s => s.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
    
    [Fact]
    public async Task GetById_Student()
    {
        //Act
        var entity = await AppDbContextSUT.StudentEntities
            .SingleAsync(i => i.Id == StudentSeeds.StudentEntity1.Id);

        //Assert
        DeepAssert.Equal(StudentSeeds.StudentEntity1 with { RegisteredSubjects = [] }, entity);
    }
    
    [Fact]
    public async Task Update_Student_Persisted()
    {
        //Arrange
        var entity1 = StudentSeeds.EmptyStudentEntity with
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        AppDbContextSUT.StudentEntities.Update(entity1);
        await AppDbContextSUT.SaveChangesAsync();

        //Act
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var entity = await dbx
                .StudentEntities
                .SingleAsync(i => i.Id == entity1.Id);

            entity.FirstName = "UPDATED FirstName";
            await dbx.SaveChangesAsync();
        }

        //Assert
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var updatedEntity = await dbx
                .StudentEntities
                .SingleAsync(i => i.Id == entity1.Id);
            Assert.Equal("UPDATED FirstName", updatedEntity.FirstName);
        }
    }
    
    [Fact]
    public async Task Delete_StudentsWithoutSubjects_Deleted()
    {
        //Arrange
        var baseEntity = StudentSeeds.StudentEntityDelete;

        //Act
        AppDbContextSUT.StudentEntities.Remove(baseEntity);
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await AppDbContextSUT.StudentEntities.AnyAsync(i => i.Id == baseEntity.Id));
    }
    
    [Fact]
    public async Task DeleteById_StudentWithoutSubjects_Deleted()
    {
        //Arrange
        var baseEntity = StudentSeeds.StudentEntityDelete;

        //Act
        AppDbContextSUT.Remove(
            AppDbContextSUT.StudentEntities.Single(i => i.Id == baseEntity.Id));
        await AppDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await AppDbContextSUT.StudentEntities.AnyAsync(i => i.Id == baseEntity.Id));
    }
    
}