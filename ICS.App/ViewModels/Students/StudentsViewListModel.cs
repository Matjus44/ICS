using System.Collections.ObjectModel;
using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.App.ViewModels.Subjects;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;

namespace ICS.App.ViewModels.Students;

public class StudentsViewListModel(
    IStudentFacade studentFacade,
    IMessengerService messengerService,
    INavigationService navigationService,
    IAlertService alertService,
    AppOptions options)
    : ViewModelBase(messengerService, options)
{
    /// <inheritdoc cref="SearchTerm"/>
    private string _searchTerm = string.Empty;
    /// <inheritdoc cref="LoadResults"/>
    private ObservableCollection<StudentListModel> _loadResults = [];
    /// <inheritdoc cref="SelectedSortOrder"/>
    private string _selectedSortOrder = string.Empty;
    
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
    /// All students returned from facade
    /// </summary>
    public ObservableCollection<StudentListModel> LoadResults
    {
        get => _loadResults;
        private set => SetProperty(ref _loadResults, value);
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
        OnSearchTermChanged += async (_, _) => await LoadAsync();
    }
    
    /// <summary>
    /// Selected order used for sorting Students
    /// </summary>
    public string SelectedSortOrder
    {
        get => _selectedSortOrder;
        set => SetProperty(ref _selectedSortOrder, value);
    }
    
    /// <summary>
    /// Loads students based on the search term.
    /// Saves the results to <see cref="LoadResults"/>.
    /// </summary>
    public async Task LoadAsync()
    {
        // Search for students based on the search term
        var results = await studentFacade.SearchStudentsByTermAsync(SearchTerm);

        // Update the search results
        LoadResults = new ObservableCollection<StudentListModel>(results);
    }
        
    /// <summary>
    /// Loads the student with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    private async Task LoadStudentByIdAsync(Guid id)
    {
        try
        {
            Data.SelectedStudent = await studentFacade.SearchStudentByIdAsync(id);
        }
        catch (Exception)
        {
            await alertService.DisplayAsync("Error", "Student not found.");
        }
    }
    
    /// <summary>
    /// Handles the search text changed event.
    /// </summary>
    /// <param name="e">Changed Searchterm</param>
    public async Task OnSearchTextChanged(TextChangedEventArgs e)
    {
        SearchTerm = e.NewTextValue; // Update the search term
        await LoadAsync(); // Perform the search
    }
    
        
    /// <summary>
    /// Handles the item selected event.
    /// Sets the SelectedStudent in <see cref="SharedData"/> to the tapped item.
    /// Updates the UI.
    /// </summary>
    /// <param name="e"></param>
    public async Task OnItemSelected(ItemTappedEventArgs e)
    {
        if (e.Item is not StudentListModel selectedStudent)
            return;

        await LoadStudentByIdAsync(selectedStudent.Id);
        
        await navigationService.GoToAsync( "//studentsDetail");
    }

    /// <summary>
    /// Handles the CreateStudent button. Navigates to create page.
    /// </summary>
    public async Task CreateStudentClicked()
    {
        await navigationService.GoToAsync( "//studentsCreate");
    }

    /// <summary>
    /// Handles sortOrder change. Sorts the existing <see cref="LoadResults"/> using selected order.
    /// </summary>
    public async void OnSortOrderChanged()
    {
        var students = await studentFacade.SortStudentsByOrderAsync(SelectedSortOrder);
        LoadResults = new ObservableCollection<StudentListModel>(students);
    }
}