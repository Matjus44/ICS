using System.Collections.ObjectModel;
using ICS.BL.Facades;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;
using ICS.Common.Enums;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using Xunit.Abstractions;

namespace ICS.BL.Tests;

public class SubjectFacadeTests : FacadeTestsBase
{
    private readonly ISubjectFacade _facadeSUT;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new SubjectFacade(UnitOfWorkFactory, SubjectModelMapper);
    }

    [Fact]
    public async Task Create_WithWithoutActivity_EqualsCreated()
    {
        //Arrange
        var model = new SubjectDetailModel
        {
            Name = "C# Seminar",
            CodeName = "ICS"
        };

        //Act
        var returnedModel = await _facadeSUT.SaveAsync(model);

        //Assert
        FixIds(model, returnedModel);
        DeepAssert.Equal(model, returnedModel);
    }

    [Fact]
    public async Task Create_WithNonExistingActivity_Throws()
    {
        //Arrange
        var model = new SubjectDetailModel()
        {
            Name = "C# Seminar",
            CodeName = "ICS",
            Activities = new ObservableCollection<ActivityListModel>()
            {
                new ActivityListModel()
                {
                    Id = Guid.NewGuid(),
                    Start = DateTime.UtcNow,
                    Finish = DateTime.UtcNow.AddHours(1),
                    Room = RoomName.D105,
                    Type = ActivityType.Exam,
                    ActivityDescription = "Tasting activity 1"
                }
            }
        };

        //Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task Create_WithExistingActivity_Throws()
    {
        //Arrange
        var model = new SubjectDetailModel()
        {
            Name = "C# Seminar",
            CodeName = "ICS",
            Activities = new ObservableCollection<ActivityListModel>()
            {
                new ActivityListModel()
                {
                    Id = ActivitySeeds.ActivityEntity1.Id,
                    Start = ActivitySeeds.ActivityEntity1.Start,
                    Finish = ActivitySeeds.ActivityEntity1.Finish,
                    Room = ActivitySeeds.ActivityEntity1.Room,
                    Type = ActivitySeeds.ActivityEntity1.Type,
                    ActivityDescription = ActivitySeeds.ActivityEntity1.ActivityDescription
                }
            }
        };

        //Act && Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task Create_WithExistingAndNotExistingActivity_Throws()
    {
        //Arrange
        var model = new SubjectDetailModel()
        {
            Name = "C# Seminar",
            CodeName = "ICS",
            Activities = new ObservableCollection<ActivityListModel>()
            {
                new ActivityListModel()
                {
                    Id = Guid.Empty,
                    Start = DateTime.MinValue,
                    Finish = DateTime.MinValue,
                    Room = RoomName.None,
                    Type = ActivityType.None,
                    ActivityDescription = null!
                }
            }
        };

        //Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task GetById_FromSeeded_EqualsSeeded()
    {
        //Arrange
        var detailModel = SubjectModelMapper.MapToDetailModel(SubjectSeeds.SubjectEntity1);

        //Act
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        //Arrange
        var listModel = SubjectModelMapper.MapToListModel(SubjectSeeds.SubjectEntity1);

        //Act
        var returnedModel = await _facadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_FromSeeded_DoesNotThrow()
    {
        //Arrange
        var detailModel = SubjectModelMapper.MapToDetailModel(SubjectSeeds.SubjectEntity1);
        detailModel.Name = "Changed recipe name";

        //Act & Assert
        await _facadeSUT.SaveAsync(detailModel with { Activities = [] });
    }

    [Fact]
    public async Task Update_Name_FromSeeded_Updated()
    {
        //Arrange
        var detailModel = SubjectModelMapper.MapToDetailModel(SubjectSeeds.SubjectEntity1);
        detailModel.Name = "Changed recipe name 1";

        //Act
        await _facadeSUT.SaveAsync(detailModel with { Activities = default! });

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_RemoveActivities_FromSeeded_NotUpdated()
    {
        //Arrange
        var detailModel = SubjectModelMapper.MapToDetailModel(SubjectSeeds.SubjectEntity1);
        detailModel.Activities.Clear();

        //Act
        await _facadeSUT.SaveAsync(detailModel);

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(SubjectModelMapper.MapToDetailModel(SubjectSeeds.SubjectEntity1), returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _facadeSUT.DeleteAsync(SubjectSeeds.SubjectEntity1.Id);
    }

    private static void FixIds(SubjectDetailModel expectedModel, SubjectDetailModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;
    }
}