using System.Collections.ObjectModel;
using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;

namespace ICS.App.ViewModels.Students;

public class StudentsViewDetailModel(
    IStudentFacade studentFacade,
    IAlertService alertService,
    IMessengerService messengerService,
    INavigationService navigationService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    /// <summary>
    /// Represents student selected using the list view.
    /// </summary>
    public StudentDetailModel SelectedStudent { get; private set; } = StudentDetailModel.Empty;

    /// <summary>
    /// Loads the selected student from the shared data instance <see cref="SharedData"/>
    /// </summary>
    public void LoadStudent()
    {
        SelectedStudent = Data.SelectedStudent;
    }
    
    /// <summary>
    /// Routes to the Edit page
    /// </summary>
    public async void EditStudent()
    {
        await navigationService.GoToAsync( "//studentsEdit");
    }

    /// <summary>
    /// Handles the deletion of student.
    /// </summary>
    /// <remarks>
    /// Catches exceptions thrown during deleting on facade.
    /// </remarks>
    public async void DeleteStudent()
    {
        try
        {
            await studentFacade.DeleteStudentAsync(SelectedStudent.Id);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Failed to delete student.");
        }

        await alertService.DisplayAsync("Success", "Student deleted successfully.");
        await navigationService.GoToAsync("//studentsList");
    }
}