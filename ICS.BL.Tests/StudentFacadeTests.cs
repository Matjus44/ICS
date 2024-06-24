using ICS.BL.Facades;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ICS.BL.Tests;

public class StudentFacadeTests : FacadeTestsBase
{
    private readonly IStudentFacade _studentFacadeSUT;

    public StudentFacadeTests(ITestOutputHelper output) : base(output)
    {
        _studentFacadeSUT = new StudentFacade(UnitOfWorkFactory, StudentModelMapper);
    }
    
    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new StudentDetailModel()
        {
            Id = Guid.Empty,
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var _ = await _studentFacadeSUT.SaveAsync(model);
    }
    
    [Fact]
    public async Task GetAll_Single_SeededStudent()
    {
        var students = await _studentFacadeSUT.GetAsync();
        var student = students.Single(i => i.Id == StudentSeeds.StudentEntity1.Id);

        DeepAssert.Equal(StudentModelMapper.MapToListModel(StudentSeeds.StudentEntity1), student);
    }

    [Fact]
    public async Task GetById_SeededStudent()
    {
        var ingredient = await _studentFacadeSUT.GetAsync(StudentSeeds.StudentEntity1.Id);

        DeepAssert.Equal(StudentModelMapper.MapToDetailModel(StudentSeeds.StudentEntity1), ingredient);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var ingredient = await _studentFacadeSUT.GetAsync(StudentSeeds.EmptyStudentEntity.Id);

        Assert.Null(ingredient);
    }

    [Fact]
    public async Task SeededStudent_DeleteById_Deleted()
    {
        await _studentFacadeSUT.DeleteAsync(StudentSeeds.StudentEntity1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.StudentEntities.AnyAsync(i => i.Id == StudentSeeds.StudentEntity1.Id));
    }

    [Fact]
    public async Task NewStudent_InsertOrUpdate_StudentAdded()
    {
        //Arrange
        var student = new StudentDetailModel
        {
            Id = Guid.Empty,
            FirstName = "Test",
            LastName = "Test",
        };
    
        //Act
        student = await _studentFacadeSUT.SaveAsync(student);
    
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var ingredientFromDb = await dbxAssert.StudentEntities.SingleAsync(i => i.Id == student.Id);
        DeepAssert.Equal(student, StudentModelMapper.MapToDetailModel(ingredientFromDb));
    }
    
    [Fact]
    public async Task SeededStudent_InsertOrUpdate_StudentUpdated()
    {
        //Arrange
        var student = new StudentDetailModel
        {
            Id = StudentSeeds.StudentEntity1.Id,
            FirstName = StudentSeeds.StudentEntity1.FirstName,
            LastName = StudentSeeds.StudentEntity1.LastName,
        };
        student.FirstName += "updated";
        student.LastName += "updated";
    
        //Act
        await _studentFacadeSUT.SaveAsync(student);
    
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var studentFromDb = await dbxAssert.StudentEntities.SingleAsync(i => i.Id == student.Id);
        DeepAssert.Equal(student, StudentModelMapper.MapToDetailModel(studentFromDb));
    }
}