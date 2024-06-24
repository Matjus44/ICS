using System.Collections.ObjectModel;
using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;

namespace ICS.App.ViewModels.Schedule;

public class ScheduleViewModel(
    IStudentFacade studentFacade,
    ISubjectFacade subjectFacade,
    IMessengerService messengerService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{ 
    /// <inheritdoc cref="Students"/>
    private ObservableCollection<StudentListModel> _students = [];
    /// <inheritdoc cref="RegisteredSubjects"/>
    private ObservableCollection<SubjectListModel> _registeredSubjects = [];
    /// <inheritdoc cref="Activities"/>
    private ObservableCollection<ActivityDetailModel> _activities = [];
    /// <inheritdoc cref="SelectedSortOrder"/>
    private string _selectedSortOrder = string.Empty;
    /// <inheritdoc cref="SearchTerm"/>
    private string _searchTerm = string.Empty;
    /// <inheritdoc cref="StartTime"/>
    private TimeSpan _startTime = DateTime.Now.TimeOfDay - TimeSpan.FromHours(1);
    /// <inheritdoc cref="EndTime"/>
    private TimeSpan _endTime = DateTime.Now.TimeOfDay + TimeSpan.FromHours(1);
    
    /// <summary>
    /// Represents the selected subject.
    /// </summary>
    public SubjectListModel SelectedSubject { get; set; } = SubjectListModel.Empty;
    
    /// <summary>
    /// Represents the selected student from the list view.
    /// </summary>
    public StudentListModel SelectedStudent { get; set; } = StudentListModel.Empty;
    
    /// <summary>
    /// String in the search bar entered by user.
    /// </summary>
    /// <remarks>
    /// Updates at every change.
    /// </remarks>
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (SetProperty(ref _searchTerm, value))
            {
                OnSearchTermChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    /// <summary>
    /// Event handler for search term change.
    /// </summary>
    private event EventHandler? OnSearchTermChanged;
    
    /// <summary>
    /// Adds handler for search term change.
    /// </summary>
    public void SearchTermHandler()
    {
        OnSearchTermChanged += async (_, _) => await GetRegisteredSubjects();
    }
    
    /// <summary>
    /// Represents the list of students.
    /// </summary>
    public ObservableCollection<StudentListModel> Students
    {
        get => _students;
        private set => SetProperty(ref _students, value);
    }
    
    /// <summary>
    /// Represents the list of activities.
    /// </summary>
    public ObservableCollection<ActivityDetailModel> Activities
    {
        get => _activities;
        set => SetProperty(ref _activities, value);
    }
    
    /// <summary>
    /// Represents the list of registered subjects.
    /// </summary>
    public ObservableCollection<SubjectListModel> RegisteredSubjects 
    {
        get => _registeredSubjects;
        private set => SetProperty(ref _registeredSubjects, value);
    }
    
    /// <summary>
    /// Represents the start time of the filtered activity.
    /// </summary>
    public TimeSpan StartTime
    {
        get => _startTime;
        set => SetProperty(ref _startTime, value);
    }

    /// <summary>
    /// Represents the end time of the filtered activity.
    /// </summary>
    public TimeSpan EndTime
    {
        get => _endTime;
        set => SetProperty(ref _endTime, value);
    }

    /// <summary>
    /// Represents the selected sort order.
    /// </summary>
    public string SelectedSortOrder
    {
        get => _selectedSortOrder;
        set => SetProperty(ref _selectedSortOrder, value);
    }

    /// <summary>
    /// Get all registered subjects for the selected student.
    /// </summary>
    public async Task GetRegisteredSubjects()
    {
        var results = await studentFacade.SearchStudentsByTermAsync(SelectedStudent.FirstName);
        var registeredSubjects = results.FirstOrDefault()?.RegisteredSubjects;
        if (registeredSubjects != null)
            RegisteredSubjects = new ObservableCollection<SubjectListModel>(registeredSubjects);
    }

    /// <summary>
    /// Get all activities for the selected student.
    /// </summary>
    public async Task GetAllActivities()
    {
        Activities.Clear();
        
        await GetRegisteredSubjects();
        foreach (var subject in RegisteredSubjects)
        {
            var activities = await subjectFacade.GetActivitiesForSubject(subject.Id);
            foreach (var activity in activities)
            {
                Activities.Add(activity);
            }
        }
    }
    
    /// <summary>
    /// Get filtered activities for the selected student.
    /// </summary>
    public async Task GetFilteredActivities()
    {
        Activities.Clear();
        
        await GetRegisteredSubjects();
        foreach (var subject in RegisteredSubjects)
        {
            var activities = await subjectFacade.GetActivitiesForSubject(subject.Id);
            foreach (var activity in activities)
            {
                if (activity.Start.TimeOfDay >= StartTime && activity.Finish.TimeOfDay <= EndTime)
                {
                    Activities.Add(activity);
                }
            }
        }
    }
    
    /// <summary>
    /// Load all students.
    /// </summary>
    public async Task LoadStudents()
    {
        var results = await studentFacade.GetAsync();
        
        Students = new ObservableCollection<StudentListModel>(results);
    }
    
    /// <summary>
    /// Load subject detail model by id.
    /// </summary>
    /// <param name="subjectId">ID of the subject</param>
    /// <returns><see cref="SubjectDetailModel"/>Subject model from facade</returns>
    public async Task<SubjectDetailModel> GetSubjectDetailById(Guid subjectId)
    {
        var subject = await subjectFacade.GetSubjectDetailByIdAsync(subjectId);
        
        return subject;
    }

    /// <summary>
    /// Select student from the list view.
    /// </summary>
    /// <param name="e">Selected student</param>
    public void OnStudentSelected(ItemTappedEventArgs e)
    {
        var selectedStudent = e.Item as StudentListModel;
        SelectedStudent = selectedStudent!;
        OnPropertyChanged(nameof(SelectedStudent));
    }

    /// <summary>
    /// Handle search text changed event.
    /// </summary>
    /// <param name="e">New searchterm</param>
    public async void OnSearchTextChanged(TextChangedEventArgs e)
    {
        SearchTerm = e.NewTextValue;
        if (string.IsNullOrEmpty(SearchTerm))
        {
            var emptyResult = await studentFacade.GetAsync();
            Students = new ObservableCollection<StudentListModel>(emptyResult);
            return;
        }

        var result = await studentFacade.SearchStudentsByTermAsync(SearchTerm);
        Students = new ObservableCollection<StudentListModel>(result);
    }

    /// <summary>
    /// Handle filter button clicked event.
    /// </summary>
    public async void OnSortOrderChanged()
    {
        var students = await studentFacade.SortStudentsByOrderAsync(SelectedSortOrder);
        Students = new ObservableCollection<StudentListModel>(students);
    }
}
