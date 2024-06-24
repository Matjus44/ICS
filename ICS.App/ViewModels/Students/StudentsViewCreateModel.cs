using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;

namespace ICS.App.ViewModels.Students;

public class StudentsViewCreateModel(
    IStudentFacade studentFacade,
    IMessengerService messengerService,
    INavigationService navigationService,
    IAlertService alertService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    /// <summary>
    /// First name of the student. Updates using the text input field.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Last name of the student. Updates using the text input field.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Photo URI of the student. Updates using the text input field.
    /// </summary>
    public string PhotoUri { get; set; } = string.Empty;
    
    /// <summary>
    /// Handles SaveStudent button clicked. Creates new <see cref="StudentDetailModel"/> and saves it.
    /// Catches exceptions thrown during saving on facade. After successful save, returns to the previous page.
    /// </summary>
    /// <remarks>
    /// Checks if the First name or Last name are empty - if yes, displays error message.
    /// Checks if the <see cref="PhotoUri"/> is empty - if yes, uses default Photo URI. If invalid, displays error message.
    /// </remarks>
    public async Task SaveStudentClicked()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        {
            await alertService.DisplayAsync("Error", "First name and last name must be filled");
            return;
        }
    
        // Check if PhotoUri is empty, if so, assign the default URI
        if (string.IsNullOrWhiteSpace(PhotoUri))
        {
            PhotoUri = "https://randomuser.me/api/portraits/lego/1.jpg";
        }

        // Convert the string PhotoUri to a Uri
        if (!Uri.TryCreate(PhotoUri, UriKind.Absolute, out Uri? resultUri))
        {
            await alertService.DisplayAsync("Error", "Invalid Photo URI");
            return;
        }
        
        StudentDetailModel student = new()
        {
            FirstName = FirstName,
            LastName = LastName,
            PhotoUri = resultUri
        };

        try
        {
            await studentFacade.CreateStudentAsync(student);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Failed to create student");
            return;
        }
    
        await alertService.DisplayAsync("Success", "Student created successfully");
        navigationService.SendBackButtonPressed();
    }
}