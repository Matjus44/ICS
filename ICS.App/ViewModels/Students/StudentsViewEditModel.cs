using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;

namespace ICS.App.ViewModels.Students;

public class StudentsViewEditModel(
    IStudentFacade studentFacade,
    IMessengerService messengerService,
    IAlertService alertService,
    INavigationService navigationService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    /// <summary>
    /// Represents student selected using the list view
    /// </summary>
    private static StudentDetailModel SelectedStudent { get; set; } = Data.SelectedStudent;
    
    /// <summary>
    /// First name of <see cref="SelectedStudent"/>.
    /// </summary>
    public string FirstName
    {
        get => SelectedStudent.FirstName;
        set => SelectedStudent.FirstName = value;
    }
    
    /// <summary>
    /// Last name of <see cref="SelectedStudent"/>.
    /// </summary>
    public string LastName
    {
        get => SelectedStudent.LastName;
        set => SelectedStudent.LastName = value;
    }
    
    /// <summary>
    /// Photo URI string of <see cref="SelectedStudent"/>.
    /// </summary>
    /// <remarks>
    /// If no PhotoUri is present or is null, its empty.
    /// </remarks>
    public string PhotoUri { get; set; } = SelectedStudent.PhotoUri?.ToString() ?? string.Empty;

    /// <summary>
    /// Loads the selected student from the shared data instance <see cref="SharedData"/>
    /// </summary>
    public void LoadStudent()
    {
        SelectedStudent = Data.SelectedStudent;
    }
    
    /// <summary>
    /// Saves the changes made to the SelectedStudent and returns to the List page.
    /// </summary>
    /// <remarks>
    /// Checks if the modified First or Last name is empty - if yes, displays an error.
    /// Checks if used URI is in correct format - if not, displays an error.
    /// Catches exceptions thrown during saving student in facade.
    /// </remarks>
    public async Task SaveStudentAsync()
    {
        if (FirstName == string.Empty || LastName == string.Empty)
        {
            await alertService.DisplayAsync("Error", "First name and last name must not be empty");
            return;
        }
        
        // Convert the string PhotoUri to a Uri
        if (!Uri.TryCreate(PhotoUri, UriKind.Absolute, out Uri? resultUri))
        {
            await alertService.DisplayAsync("Error", "Invalid Photo URI");
            return;
        }

        SelectedStudent.PhotoUri = resultUri;

        try
        {
            await studentFacade.UpdateStudentAsync(SelectedStudent);
            Data.SelectedStudent = SelectedStudent;
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Failed to save changes. Please try again later.");
            return;
        }
        
        await alertService.DisplayAsync("Success", "Changes successfully saved");
        await navigationService.GoToAsync("//studentsList");
    }
}