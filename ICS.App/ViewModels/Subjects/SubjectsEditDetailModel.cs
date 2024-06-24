using System.Collections.ObjectModel;
using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;
using ICS.App.ViewModels;
using ICS.Common.Enums;

namespace ICS.App.ViewModels.Subjects;

public class SubjectsEditDetailModel(
    IMessengerService messengerService,
    ISubjectFacade subjectFacade,
    IStudentFacade studentFacade,
    IAlertService alertService,
    IActivityFacade activityFacade,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    /// <inheritdoc cref="SubjectsActivities"/>
    private ObservableCollection<ActivityDetailModel> _subjectsActivities = [];
    /// <inheritdoc cref="SubjectsStudents"/>
    private ObservableCollection<StudentDetailModel> _subjectsStudents = [];
    /// <inheritdoc cref="AllStudents"/>
    private ObservableCollection<StudentListModel> _allStudents = [];
    /// <inheritdoc cref="NewActivityTime"/>
    private DateTime _newActivityTime = DateTime.Now;
    /// <inheritdoc cref="NewActivityEndTime"/>
    private DateTime _newActivityEndTime = DateTime.Now.AddHours(2);
    /// <inheritdoc cref="RoomName"/>
    private string? _newActivityRoomName = string.Empty;
    /// <inheritdoc cref="NewActivityType"/>
    private string? _newActivityType = string.Empty;
    /// <inheritdoc cref="NewActivityDescription"/>
    private string? _newActivityDescription = string.Empty;
    /// <inheritdoc cref="UnregisterButtonVisible"/>
    private bool _unregisterButtonVisible;
    /// <inheritdoc cref="RegisterButtonVisible"/>
    private bool _registerButtonVisible;
    /// <inheritdoc cref="DeleteActivityButtonVisible"/>
    private bool _deleteActivityButtonVisible;
    
    /// <summary>
    /// Currently selected subject.
    /// </summary>
    public SubjectDetailModel Subject { get; private set; } = SubjectDetailModel.Empty;
    
    /// <summary>
    /// Currently selected activity.
    /// </summary>
    public ActivityDetailModel SelectedActivity { get; set; } = ActivityDetailModel.Empty;
    
    /// <summary>
    /// Currently selected student.
    /// </summary>
    public StudentDetailModel SelectedStudent { get; set; } = StudentDetailModel.Empty;
    
    /// <summary>
    /// Student selected to register.
    /// </summary>
    public StudentListModel SelectedToRegisterStudent { get; set; } = StudentListModel.Empty;
    
    /// <summary>
    /// All students in the system.
    /// </summary>
    public ObservableCollection<StudentListModel> AllStudents
    {
        get => _allStudents;
        private set => SetProperty(ref _allStudents, value);
    }

    /// <summary>
    /// Date and time of the start of the new activity.
    /// </summary>
    public DateTime NewActivityTime
    {
        get => _newActivityTime;
        set => SetProperty(ref _newActivityTime, value);
    }
    
    /// <summary>
    /// Date and time of the end of the new activity.
    /// </summary>
    public DateTime NewActivityEndTime
    {
        get => _newActivityEndTime;
        set => SetProperty(ref _newActivityEndTime, value);
    }

    /// <summary>
    /// Room name of the new activity.
    /// </summary>
    public string? NewActivityRoomName
    {
        get => _newActivityRoomName;
        set => SetProperty(ref _newActivityRoomName, value);
    }
    
    /// <summary>
    /// Type of the new activity.
    /// </summary>
    public string? NewActivityType
    {
        get => _newActivityType;
        set => SetProperty(ref _newActivityType, value);
    }
    
    /// <summary>
    /// Description of the new activity.
    /// </summary>
    public string? NewActivityDescription
    {
        get => _newActivityDescription;
        set => SetProperty(ref _newActivityDescription, value);
    }
    
    /// <summary>
    /// Visibility of the unregister button.
    /// </summary>
    public bool UnregisterButtonVisible
    {
        get => _unregisterButtonVisible;
        private set => SetProperty(ref _unregisterButtonVisible, value);
    }
    
    /// <summary>
    /// Visibility of the register button.
    /// </summary>
    public bool RegisterButtonVisible
    {
        get => _registerButtonVisible;
        private set => SetProperty(ref _registerButtonVisible, value);
    }
    
    /// <summary>
    /// Visibility of the delete activity button.
    /// </summary>
    public bool DeleteActivityButtonVisible
    {
        get => _deleteActivityButtonVisible;
        private set => SetProperty(ref _deleteActivityButtonVisible, value);
    }
    
    /// <summary>
    /// Students in the selected subject.
    /// </summary>
    public ObservableCollection<StudentDetailModel> SubjectsStudents
    {
        get => _subjectsStudents;
        private set => SetProperty(ref _subjectsStudents, value);
    }
    
    /// <summary>
    /// Activities in the selected subject.
    /// </summary>
    public ObservableCollection<ActivityDetailModel> SubjectsActivities
    {
        get => _subjectsActivities;
        private set => SetProperty(ref _subjectsActivities, value);
    }
    
    /// <summary>
    /// Loads students for the selected subject.
    /// </summary>
    public async Task LoadStudentsFromSubject()
    {
        {
            Subject = SharedData.Instance.SelectedSubject;
            var students = await studentFacade.GetStudentsBySubjectId(Subject.Id);
            SubjectsStudents = new ObservableCollection<StudentDetailModel>(students);
        }
    }

    /// <summary>
    /// Loads activities for the selected subject.
    /// </summary>
    public async Task LoadActivitiesFromSubject()
    {
        {
            Subject = SharedData.Instance.SelectedSubject;
            var activities = await subjectFacade.GetActivitiesForSubject(Subject.Id);
            SubjectsActivities = new ObservableCollection<ActivityDetailModel>(activities);
        }
    }

    /// <summary>
    /// Unregisters the selected student from the subject.
    /// Catches any exceptions during unregister and displays an error message.
    /// </summary>
    public async Task OnUnregisterStudentClicked()
    {
        try
        {
            await subjectFacade.UnregisterStudentFromSubject(Subject.Id, SelectedStudent.Id);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Student deletion failed");
            return;
            
        }
        
        var students = await studentFacade.GetStudentsBySubjectId(Subject.Id);
        SubjectsStudents = new ObservableCollection<StudentDetailModel>(students);
        SelectedStudent = StudentDetailModel.Empty;
        UnregisterButtonVisible = false;
        await alertService.DisplayAsync("Success", "Student unregistered successfully");
    }

    /// <summary>
    /// Loads all students.
    /// </summary>
    public async void LoadAllStudents()
    {
        var students = await studentFacade.GetAsync();
        AllStudents = new ObservableCollection<StudentListModel>(students);
    }
    
    /// <summary>
    /// Registers the selected student to the subject.
    /// Catches any exceptions during registration and displays an error message.
    /// </summary>
    public async void OnRegisterStudentClicked()
    {
        try
        {
            await subjectFacade.RegisterStudentToSubject(Subject.Id, SelectedToRegisterStudent.Id);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Student registration failed");
            return;
        }
        
        var students = await studentFacade.GetStudentsBySubjectId(Subject.Id);
        SubjectsStudents = new ObservableCollection<StudentDetailModel>(students);
        SelectedToRegisterStudent = StudentListModel.Empty;
        RegisterButtonVisible = false;
        await alertService.DisplayAsync("Success", "Student registered successfully");
    }
    
    /// <summary>
    /// Handles the student selected event.
    /// </summary>
    /// <param name="e">Selected student from the list</param>
    public void OnStudentSelected(ItemTappedEventArgs e)
    {
        if (e.Item is not StudentDetailModel selectedStudent) return;
        
        SelectedStudent = selectedStudent;
        UnregisterButtonVisible = true;
    }

    /// <summary>
    /// Parses the activity type from the string.
    /// </summary>
    /// /// <remarks>
    /// If the parsing fails, returns <see cref="ActivityType.None"/>.
    /// </remarks>
    /// <param name="type">String representation of the <see cref="ActivityType"/></param>
    /// <returns><see cref="ActivityType"/> enum or None</returns>
    private ActivityType ParseActivityType(string type)
    {
        try
        {
            if (Enum.TryParse<ActivityType>(type, out var typeEnum))
            {
                return typeEnum;
            }
        }
        catch (Exception)
        {
            return ActivityType.None;
        }

        return ActivityType.None;
    }
    
    /// <summary>
    /// Parses the room name from the string.
    /// </summary>
    /// <remarks>
    /// If the parsing fails, returns <see cref="RoomName.None"/>.
    /// </remarks>
    /// <param name="room">String representation of the <see cref="RoomName"/></param>
    /// <returns></returns>
    private RoomName ParseRoomName(string room)
    {
        try
        {
            if (Enum.TryParse<RoomName>(room, out var roomEnum))
            {
                return roomEnum;
            }
        }
        catch (Exception)
        {
            return RoomName.None;
        }

        return RoomName.None;
    }
    
    /// <summary>
    /// Handles the activity add clicked event. Checks if all fields are filled in.
    /// Catch any exceptions during activity creation and display an error message.
    /// </summary>
    public async Task OnActivityAddClicked()
    {
        if (NewActivityRoomName == string.Empty || NewActivityType == string.Empty || NewActivityDescription == string.Empty)
        {
            await alertService.DisplayAsync("Error", "Please fill in all fields");
            return;
        }
        
        var newActivity = new ActivityDetailModel
        {
            Id = Guid.NewGuid(),
            ActivityDescription = NewActivityDescription!,
            Start = NewActivityTime,
            Finish = NewActivityEndTime,
            SubjectId = Subject.Id,
            Room = ParseRoomName(NewActivityRoomName!),
            Type = ParseActivityType(NewActivityType!)
        };

        try
        {
            await activityFacade.CreateActivityAsync(newActivity);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Activity creation failed");
            return;
        }
        
        SubjectsActivities.Add(newActivity);
        var activities = await subjectFacade.GetActivitiesForSubject(Subject.Id);
        SubjectsActivities = new ObservableCollection<ActivityDetailModel>(activities);
        await alertService.DisplayAsync("Success","Activity created successfully");
    }
        
    /// <summary>
    /// Handles the student to register clicked event.
    /// </summary>
    /// <param name="e">Selected student</param>
    public void OnStudentToRegisterClicked(ItemTappedEventArgs e)
    {
        if (e.Item is not StudentListModel student) return;
        
        SelectedToRegisterStudent = student;
        RegisterButtonVisible = true;
    }

    /// <summary>
    /// Handles the activity selected event.
    /// </summary>
    /// <param name="e">Selected activity</param>
    public void OnActivitySelected(ItemTappedEventArgs e)
    {
        if (e.Item is not ActivityDetailModel activity) return;
        
        SelectedActivity = activity;
        DeleteActivityButtonVisible = true;
    }

    /// <summary>
    /// Handles the activity delete clicked event.
    /// Catch any exceptions during activity deletion and display an error message.
    /// </summary>
    public async void OnActivityDeleteClicked()
    {
        try
        {
            await activityFacade.DeleteActivityAsync(SelectedActivity.Id);
            
        } 
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Activity deletion failed");
            return;
        }
        
        var activities = await subjectFacade.GetActivitiesForSubject(Subject.Id);
        SubjectsActivities = new ObservableCollection<ActivityDetailModel>(activities);
        DeleteActivityButtonVisible = false;
        await alertService.DisplayAsync("Success", "Activity deleted successfully");
    }

    /// <summary>
    /// Handles the save clicked event.
    /// </summary>
    public async void OnSaveClicked()
    {
        try
        {
            await subjectFacade.SaveAsync(Subject);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Subject update failed");
            return;
        }
        
        await alertService.DisplayAsync("Success", "Subject updated successfully");
    }

    /// <summary>
    /// Handles the edit activity clicked event.
    /// </summary>
    public void OnEditActivityClicked()
    {
        NewActivityTime = SelectedActivity.Start;
        NewActivityEndTime = SelectedActivity.Finish;
        NewActivityRoomName = SelectedActivity.Room.ToString();
        NewActivityType = SelectedActivity.Type.ToString();
        NewActivityDescription = SelectedActivity.ActivityDescription;
    }

    /// <summary>
    /// Handles the save activity clicked event.
    /// </summary>
    public async void OnSaveActivityClicked()
    {
        SelectedActivity.Start = NewActivityTime;
        SelectedActivity.Finish = NewActivityEndTime;
        SelectedActivity.Room = ParseRoomName(NewActivityRoomName!);
        SelectedActivity.Type = ParseActivityType(NewActivityType!);
        SelectedActivity.ActivityDescription = NewActivityDescription!;
        OnPropertyChanged(nameof(SelectedActivity));

        try
        {
            await activityFacade.SaveAsync(SelectedActivity);
        } 
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Activity update failed");
            return;
        }
        
        await alertService.DisplayAsync("Success", "Activity updated successfully");
    }
}