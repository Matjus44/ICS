using System.Collections.ObjectModel;
using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;
using ICS.App.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ICS.App.ViewModels.Subjects;

public class SubjectsViewDetailModel(
    IMessengerService messengerService,
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IAlertService alertService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    /// <inheritdoc cref="StartTime"/>
    private TimeSpan _startTime = DateTime.Now.TimeOfDay - TimeSpan.FromHours(1);
    /// <inheritdoc cref="EndTime"/>
    private TimeSpan _endTime = DateTime.Now.TimeOfDay + TimeSpan.FromHours(1);
    /// <inheritdoc cref="SubjectName"/>
    private string _subjectName = string.Empty;
    /// <inheritdoc cref="SubjectCodeName"/>
    private string _subjectCodeName = string.Empty;
    /// <inheritdoc cref="SearchTerm"/>
    private string _searchTerm = string.Empty;
    /// <inheritdoc cref="LoadedSubjects"/>
    private ObservableCollection<SubjectDetailModel> _loadedSubjects = [];
    /// <inheritdoc cref="Activities"/>
    private ObservableCollection<ActivityDetailModel> _activities = [];
    /// <inheritdoc cref="SelectedSortOrder"/>
    private string _selectedSortOrder = string.Empty;
    /// <inheritdoc cref="EditButtonVisible"/>
    private bool _editButtonVisible = false;
    
    /// <summary>
    /// Start time for filtering activities.
    /// </summary>
    public TimeSpan StartTime
    {
        get => _startTime;
        set => SetProperty(ref _startTime, value);
    }

    /// <summary>
    /// End time for filtering activities.
    /// </summary>
    public TimeSpan EndTime
    {
        get => _endTime;
        set => SetProperty(ref _endTime, value);
    }
    
    /// <summary>
    /// Represents subject selected using the list view
    /// </summary>
    public SubjectDetailModel SelectedSubject { get; set; } = SubjectDetailModel.Empty;
    
    /// <summary>
    /// All subjects returned from facade
    /// </summary>
    public ObservableCollection<SubjectDetailModel> LoadedSubjects
    {
        get => _loadedSubjects;
        private set => SetProperty(ref _loadedSubjects, value);
    }
    
    /// <summary>
    /// All activities associated with selected subject <see cref="SelectedSubject"/>
    /// </summary>
    public ObservableCollection<ActivityDetailModel> Activities
    {
        get => _activities;
        set => SetProperty(ref _activities, value);
    }
    
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
    /// Name of the subject to be created.
    /// </summary>
    public string SubjectName
    {
        get => _subjectName;
        set => SetProperty(ref _subjectName, value);
    }
    
    /// <summary>
    /// Codename of the subject to be created.
    /// </summary>
    public string SubjectCodeName
    {
        get => _subjectCodeName;
        set => SetProperty(ref _subjectCodeName, value);
    }
    
    /// <summary>
    /// Visibility of the edit button.
    /// </summary>
    public bool EditButtonVisible
    {
        get => _editButtonVisible;
        set => SetProperty(ref _editButtonVisible, value);
    }

    /// <summary>
    /// Selected order used for sorting Subjects
    /// </summary>
    public string SelectedSortOrder
    {
        get => _selectedSortOrder;
        set => SetProperty(ref _selectedSortOrder, value);
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
        OnSearchTermChanged += async (_, _) => await LoadAllSubjectsAsync();
    }
    
    /// <summary>
    /// Loads all subjects currently in DB and stores them in <see cref="LoadedSubjects"/>
    /// </summary>
    public async Task LoadAllSubjectsAsync()
    {
        var results = await subjectFacade.GetAllSubjectsAsync();
        LoadedSubjects = new ObservableCollection<SubjectDetailModel>(results);
    }
    
    /// <summary>
    /// Loads all Activities based on currently selected subject.
    /// </summary>
    /// <remarks>
    /// Before every search clears old activities
    /// </remarks>
    private async Task LoadAllActivitiesAsync()
    {
        Activities.Clear();        
        var results = await subjectFacade.GetActivitiesForSubject(SelectedSubject.Id);
        // Update the search results
        foreach (var activity in results)
        {
            if (Activities.All(a => a.Id != activity.Id))
            {
                Activities.Add(activity);        
            }
        }
    }
    
    /// <summary>
    /// Is called when user clicks on item in list view.
    /// Loads all activities for set subject. <see cref="LoadAllActivitiesAsync"/>
    /// </summary>
    /// <param name="e">Selected item</param>
    public async void OnItemSelected(ItemTappedEventArgs e)
    {
        if (e.Item is SubjectDetailModel selectedSubject)
        {
            SelectedSubject = selectedSubject;
            SharedData.Instance.SelectedSubject = selectedSubject;
            await LoadAllActivitiesAsync();
            OnPropertyChanged(nameof(SelectedSubject));
        }
    }
    
    /// <summary>
    /// Searches for SubjectDetailModel by ID. Used for displaying activities in grid.
    /// </summary>
    /// <param name="subjectId">ID of wanted subject</param>
    /// <returns>SubjectDetailModel of requested subject or Empty if not found</returns>
    /// <exception cref="ArgumentException">If subject is not found</exception>
    public async Task<SubjectDetailModel> GetSubjectDetailById(Guid subjectId)
    {
        try
        {
            var subject = await subjectFacade.GetSubjectDetailByIdAsync(subjectId);
            return subject;
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Subject not found");
        }
        return SubjectDetailModel.Empty;
    }
    
    /// <summary>
    /// Is called everytime text in searchbar is updated.
    /// Updates <see cref="LoadedSubjects"/> by searching for subjects by <see cref="SearchTerm"/>.
    /// </summary>
    /// <param name="e">New searchterm</param>
    public async void OnSearchTextChanged(TextChangedEventArgs e)
    {
        SearchTerm = e.NewTextValue; // Update the search term
        if (string.IsNullOrWhiteSpace(SearchTerm))
        {
            return;
        }
        
        var result = await subjectFacade.SearchSubjectsByNameAsync(SearchTerm);
        LoadedSubjects = new ObservableCollection<SubjectDetailModel>(result);
    }

    /// <summary>
    /// Navigates to the edit page.
    /// </summary>
    public async Task OnEditClicked()
    {
        await navigationService.GoToAsync( "/edit");
    }

    /// <summary>
    /// Creates new subject and saves it. Catches exceptions during saving and displays error message.
    /// </summary>
    /// <remarks>
    /// Clears the input fields and refreshes after successful creation.
    /// </remarks>
    public async Task CreateSubject()
    {
        if (SubjectName == String.Empty || SubjectCodeName == String.Empty) 
        {
            await alertService.DisplayAsync("Error", "SubjectName and code name must be filled in");
            return;
        }
        
        var newSubject = new SubjectDetailModel
        {
            Id = Guid.NewGuid(),
            Name = SubjectName,
            CodeName = SubjectCodeName,
            RegisteredStudents = [],
            Activities = []
        };
        
        try
        {
            await subjectFacade.CreateSubjectAsync(newSubject);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Subject creation failed");
            return;
        }
    
        await alertService.DisplayAsync("Success","Subject created successfully");
        await LoadAllSubjectsAsync();
        SubjectName = String.Empty;
        SubjectCodeName = String.Empty;
    }

    /// <summary>
    /// Sets the selected order for sorting subjects and refreshes the list.
    /// </summary>
    /// <remarks>
    /// Sorting is done by <see cref="ISubjectFacade.SortSubjectsByOrderAsync"/>
    /// </remarks>
    /// <param name="selectedOrder">Sort order to be used</param>
    public async void OrderSelectChanged(string selectedOrder)
    {
        SelectedSortOrder = selectedOrder;
        var subjects = await subjectFacade.SortSubjectsByOrderAsync(SelectedSortOrder);
        LoadedSubjects = new ObservableCollection<SubjectDetailModel>(subjects);
    }

    /// <summary>
    /// Filters activities based on the selected subject and time range.
    /// </summary>
    public async Task OnFilterButtonClicked()
    {
        Activities.Clear();
        var activities = await subjectFacade.GetActivitiesForSubject(SelectedSubject.Id);
        foreach (var activity in activities)
        {
            if (activity.Start.TimeOfDay >= StartTime && activity.Finish.TimeOfDay <= EndTime)
            {
                Activities.Add(activity);
            }
        }
    }

    public async Task OnDeleteClicked()
    {
        try
        {
            var subjectToRemove = await subjectFacade.GetSubjectDetailByIdAsync(SelectedSubject.Id);
            await subjectFacade.DeleteSubjectAsync(subjectToRemove.Id);
        }catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Subject deletion failed");
            return;
        }
        LoadedSubjects.Remove(SelectedSubject);
        SelectedSubject = SubjectDetailModel.Empty;
        EditButtonVisible = false;
        await alertService.DisplayAsync("Success", "Subject deleted successfully");
    }
}