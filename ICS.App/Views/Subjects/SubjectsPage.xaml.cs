using ICS.App.ViewModels.Subjects;
using ICS.BL.Models;
namespace ICS.App.Views.Subjects;

public partial class SubjectsPage
{
    private readonly SubjectsViewDetailModel _viewDetailModelPage;
    public SubjectsPage(SubjectsViewDetailModel viewDetailModelPage)
        : base(viewDetailModelPage)
    {
        _viewDetailModelPage = viewDetailModelPage;
        _viewDetailModelPage.SearchTermHandler();
        InitializeComponent();
    }
    protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewDetailModelPage.SelectedSortOrder = string.Empty;
            await _viewDetailModelPage.LoadAllSubjectsAsync();
            ScheduleGrid = this.FindByName<Grid>("ScheduleGrid");
            _viewDetailModelPage.EditButtonVisible = false;
            RemoveAllActivitiesFromGrid();
        }
    
    private void RemoveAllActivitiesFromGrid()
    {
        var dynamicActivities = ScheduleGrid.Children.Where(child => child is Frame && ((Frame)child).AutomationId?.StartsWith("DynamicLabel") == true).ToList();
        foreach (var frame in dynamicActivities)
        {
            ScheduleGrid.Children.Remove(frame);
        }
    }
    

    private void OnItemSelected(object sender, ItemTappedEventArgs e)
    {
        _viewDetailModelPage.OnItemSelected(e);
        DisplayActivitiesInScheduleGrid();
        _viewDetailModelPage.EditButtonVisible = true;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewDetailModelPage.OnSearchTextChanged(e);
    }

    private void DisplayActivitiesInScheduleGrid()
    {
        var framesToRemove = ScheduleGrid.Children
            .Where(child => child is Frame frame && frame.AutomationId?
            .StartsWith("DynamicLabel") == true)
            .ToList();
        foreach (var frame in framesToRemove)
        {
            ScheduleGrid.Children.Remove(frame);
        }
        
        foreach (var activity in _viewDetailModelPage.Activities)
        {
            int row = GetRow(activity);
            int startColumn = GetStartColumn(activity);
            int endColumn = GetEndColumn(activity);

            SubjectDetailModel subject = _viewDetailModelPage.GetSubjectDetailById(activity.SubjectId).Result;

            var frame = new Frame
            {
                Content = new Label
                {
                    Text = subject.CodeName + "\n" + activity.Room + "\n" + activity.Type,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                },
                BackgroundColor = Colors.CornflowerBlue,
                BorderColor = Colors.Black,
                HasShadow = false,
                Padding = new Thickness(1),
                AutomationId = "DynamicLabel",
            };
            
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, startColumn);
            Grid.SetColumnSpan(frame, endColumn - startColumn);

            ScheduleGrid.Children.Add(frame);
        }
    }
    
    private async void OnEditClicked(object sender, EventArgs e)
    {
        await _viewDetailModelPage.OnEditClicked();
    }

    private async void CreateSubject(object? sender, EventArgs e)
    {
        await _viewDetailModelPage.CreateSubject();
    }

    private void OrderSelectChanged(object? sender, EventArgs e)
    {
        var picker = (Picker) sender!;
        var selectedSortOrder = picker.SelectedItem.ToString();
        if (selectedSortOrder != null) _viewDetailModelPage.OrderSelectChanged(selectedSortOrder);
    }
    
    
    private async void OnFilterButtonClicked(object? sender, EventArgs e)
    {
        await _viewDetailModelPage.OnFilterButtonClicked();
        DisplayActivitiesInScheduleGrid();
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        await _viewDetailModelPage.OnDeleteClicked();
        _viewDetailModelPage.SelectedSubject = SubjectDetailModel.Empty;
        RemoveAllActivitiesFromGrid();
        
    }
}