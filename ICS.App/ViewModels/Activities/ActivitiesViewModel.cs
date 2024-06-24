using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;
using ICS.Common.Enums;
using System.Collections.ObjectModel;

namespace ICS.App.ViewModels.Activities;

public class ActivitiesViewModel(
    IStudentFacade studentFacade,
    ISubjectFacade subjectFacade,
    IActivityFacade activityFacade,
    IRatingFacade ratingFacade,
    IMessengerService messengerService,
    IAlertService alertService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    private ObservableCollection<SubjectListModel> _subjects = new();
    private ObservableCollection<ActivityDetailModel> _activities = new();
    private ObservableCollection<RatingDetailModel> _ratings = new();
    private ObservableCollection<StudentDetailModel> _students = new();
    private SubjectListModel _selectedSubject = SubjectListModel.Empty;
    private ActivityDetailModel _selectedActivity = ActivityDetailModel.Empty;
    private StudentDetailModel _selectedStudent = StudentDetailModel.Empty;
    private RatingDetailModel _selectedRating = RatingDetailModel.Empty;
    private bool _deleteButtonVisible;
    private bool _editButtonVisible;
    private bool _editActivityButtonVisible;
    private DateTime _startTime = DateTime.Now;
    private DateTime _endTime = DateTime.Now.AddHours(2);
    private string _room = String.Empty;
    private string _type = String.Empty;
    private string _description = String.Empty;
    private string _orderType = "Start";
    private string _selectedRatingOrderType = string.Empty;
    private string _selectedActivityOrderType = string.Empty;
    private string _rating = String.Empty;

    public bool EditActivityButtonVisible
    {
        get => _editActivityButtonVisible;
        set => SetProperty(ref _editActivityButtonVisible, value);
    }
    
    public string SelectedRatingOrderType
    {
        get => _selectedRatingOrderType;
        set => SetProperty(ref _selectedRatingOrderType, value);
    }
    
    public string SelectedActivityOrderType
    {
        get => _selectedActivityOrderType;
        set => SetProperty(ref _selectedActivityOrderType, value);
    }
    
    
    public bool EditButtonVisible
    {
        get => _editButtonVisible;
        set => SetProperty(ref _editButtonVisible, value);
    }
    
    public bool DeleteButtonVisible
    {
        get => _deleteButtonVisible;
        set => SetProperty(ref _deleteButtonVisible, value);
    }
    
    public RatingDetailModel SelectedRating
    {
        get => _selectedRating;
        set => SetProperty(ref _selectedRating, value);
    }
    
    
    public string Rating
    {
        get => _rating;
        set => SetProperty(ref _rating, value);
    }
    

    public ObservableCollection<StudentDetailModel> Students
    {
        get => _students;
        set => SetProperty(ref _students, value);
    }
    
    public ObservableCollection<RatingDetailModel> Ratings
    {
        get => _ratings;
        set => SetProperty(ref _ratings, value);
    }
    
    public ObservableCollection<ActivityDetailModel> Activities
    {
        get => _activities;
        set => SetProperty(ref _activities, value);
    }
    public ObservableCollection<SubjectListModel> Subjects
    {
        get => _subjects;
        set => SetProperty(ref _subjects, value);
    }
    public SubjectListModel SelectedSubject
    {
        get => _selectedSubject;
        set => SetProperty(ref _selectedSubject, value);
    }
    public ActivityDetailModel SelectedActivity
    {
        get => _selectedActivity;
        set => SetProperty(ref _selectedActivity, value);
    }
    public StudentDetailModel SelectedStudent
    {
        get => _selectedStudent;
        set => SetProperty(ref _selectedStudent, value);
    }
    public DateTime StartTime
    {
        get => _startTime;
        set => SetProperty(ref _startTime, value);
    }
    public DateTime EndTime
    {
        get => _endTime;
        set => SetProperty(ref _endTime, value);
    }
    public string Room
    {
        get => _room;
        set => SetProperty(ref _room, value);
    }
    public string Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
    public string OrderType
    {
        get => _orderType;
        set => SetProperty(ref _orderType, value);
    }


    private async Task GetStudentsRatingsInSubject()
    {
        var ratings = await ratingFacade.GetStudentsRatingsInSubjectAsync(SelectedStudent.Id, SelectedSubject.Id);
        Ratings = new ObservableCollection<RatingDetailModel>(ratings);
    }
    
    public async Task GetAllSubjects()
    {
        var subjects = await subjectFacade.GetAsync();
        Subjects = new ObservableCollection<SubjectListModel>(subjects);
    }

    private async Task GetSubjectsStudents()
    {
        var students = await studentFacade.GetStudentsBySubjectId(SelectedSubject.Id);
        Students = new ObservableCollection<StudentDetailModel>(students);
    }

    private async Task GetActivitiesForSubject(Guid subjectId)
    {
        Activities.Clear();
        IEnumerable<ActivityDetailModel> activities = await subjectFacade.GetActivitiesForSubject(subjectId);
        Activities = new ObservableCollection<ActivityDetailModel>(activities);
    }

    private RoomName ParseRoomName(string room)
    {
        if (Enum.TryParse<RoomName>(room, out var roomEnum))
        {
            return roomEnum;
        }

        return RoomName.None;
    }
    
    private ActivityType ParseActivityType(string type)
    {
        if (Enum.TryParse<ActivityType>(type, out var typeEnum))
        {
            return typeEnum;
        }

        return ActivityType.None;
    }

    public async Task OnItemSelected(ItemTappedEventArgs itemTappedEventArgs)
    {
        SelectedSubject = (itemTappedEventArgs.Item as SubjectListModel)!;
        SelectedStudent = StudentDetailModel.Empty;
        await GetSubjectsStudents();
        await GetActivitiesForSubject(SelectedSubject.Id);
    }
    
    public async Task OnStudentSelected(ItemTappedEventArgs e)
    {
        if (e.Item is not StudentDetailModel student) return;
        SelectedStudent = student;
        
        await GetStudentsRatingsInSubject();
        
    }

    public async void OnAddRatingClicked()
    {
        if(Description == string.Empty || Rating == string.Empty || SelectedActivity.Id == Guid.Empty || SelectedStudent.Id == Guid.Empty || SelectedSubject.Id == Guid.Empty)
        {
            await alertService.DisplayAsync("Error", "Please fill in all fields");
            return;
        }

        if (SelectedActivity.Type != ActivityType.Exam)
        {
            await alertService.DisplayAsync("Error", "Only exams can be rated");
            return;
        }
        
        if (!int.TryParse(Rating, out int parsedRating))
        {
            await alertService.DisplayAsync("Error", "Rating must be a valid integer");
            return;
        }

        if (parsedRating is < 1 or > 5)
        {
            await alertService.DisplayAsync("Error", "Rating must be between 1 and 5");
            return;
        }

        var existingRating = Ratings.FirstOrDefault(r => r.ActivityId == SelectedActivity.Id && r.StudentId == SelectedStudent.Id);
        if (existingRating != null)
        {
            await alertService.DisplayAsync("Error", "This activity already has a rating for the selected student.");
            return;
        }
        
        var newRating = new RatingDetailModel
        {
            Id = Guid.NewGuid(),
            ActivityId = SelectedActivity.Id,
            StudentId = SelectedStudent.Id,
            Note = Description,
            Rating = parsedRating
        };
        
        try
        {
            await ratingFacade.AddRatingAsync(newRating);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Rating creation failed");
            return;
        }
        
        await alertService.DisplayAsync("Success", "Rating created successfully");
        Ratings.Add(newRating);
        Description = string.Empty;
        Rating = string.Empty;
    }

    public void OnActivitySelected(ItemTappedEventArgs e)
    {
        if (e.Item is ActivityDetailModel activity)
        {
            SelectedActivity = activity;
        }
    }

    public void OnRatingClicked(ItemTappedEventArgs e)
    {
        if (e.Item is RatingDetailModel rating)
        {
            SelectedRating = rating;
        }

        EditButtonVisible = true;
        DeleteButtonVisible = true;
    }

    public async void OnDeleteRatingClicked()
    {
        try
        {
            await ratingFacade.DeleteAsync(SelectedRating.Id);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Activity update failed");
        }
        
        await alertService.DisplayAsync("Success", "Activity updated successfully");
        Ratings.Remove(SelectedRating);
        SelectedRating = RatingDetailModel.Empty;
        Rating = string.Empty;
        Description = string.Empty;
    }
    
    public void OnEditRatingClicked()
    {
        Description = SelectedRating.Note;
        Rating = SelectedRating.Rating.ToString();
    }
    
    public async void OnSaveEditRatingClicked()
    {
        if(Description == string.Empty || Rating == string.Empty)
        {
            await alertService.DisplayAsync("Error", "Please fill in all fields");
            return;
        }

        var updatedRating = new RatingDetailModel
        {
            Id = SelectedRating.Id,
            ActivityId = SelectedRating.ActivityId,
            StudentId = SelectedRating.StudentId,
            Note = Description,
            Rating = int.Parse(Rating)
        };
        
        try
        {
            await ratingFacade.SaveAsync(updatedRating);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Rating update failed");
            return;
        }
        
        await alertService.DisplayAsync("Success", "Rating updated successfully");
        await GetStudentsRatingsInSubject();
    }

    public async Task OnSortActivityChanged()
    {
        var activities = await activityFacade.SortActivitiesAsync(SelectedActivityOrderType, SelectedSubject.Id);
        Activities = new ObservableCollection<ActivityDetailModel>(activities);
    }

    public async Task OnSortRatingChanged()
    {
        var ratings = await ratingFacade.SortStudentsRatingsInActivityAsync(SelectedRatingOrderType, SelectedStudent.Id, SelectedSubject.Id);
        Ratings = new ObservableCollection<RatingDetailModel>(ratings);
    }
}