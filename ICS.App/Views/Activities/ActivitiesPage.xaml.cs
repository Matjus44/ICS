using ICS.App.ViewModels.Activities;
using ICS.BL.Models;

namespace ICS.App.Views.Activities;

public partial class ActivitiesPage
{
    private readonly ActivitiesViewModel _viewModelPage;
    public ActivitiesPage(ActivitiesViewModel viewModelPage)
        : base(viewModelPage)
    {
        _viewModelPage = viewModelPage;
        InitializeComponent();
        
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModelPage.SelectedActivityOrderType = string.Empty;
        _viewModelPage.SelectedRatingOrderType = string.Empty;
        _viewModelPage.SelectedActivity = ActivityDetailModel.Empty;
        _viewModelPage.SelectedRating = RatingDetailModel.Empty;
        _viewModelPage.SelectedStudent = StudentDetailModel.Empty;
        await _viewModelPage.GetAllSubjects();
        
    }

    private async void OnItemSelected(object? sender, ItemTappedEventArgs e)
    {
        await _viewModelPage.OnItemSelected(e);
    }

    private async void OnStudentSelected(object? sender, ItemTappedEventArgs e)
    {
        await _viewModelPage.OnStudentSelected(e);
    }

    private void OnAddRatingClicked(object? sender, EventArgs e)
    {
        _viewModelPage.OnAddRatingClicked();
    }

    private void OnEditRatingClicked(object? sender, EventArgs e)
    {
        _viewModelPage.OnEditRatingClicked();
    }

    private void OnActivitySelected(object? sender, ItemTappedEventArgs e)
    {
        _viewModelPage.OnActivitySelected(e);
    }

    private void OnDeleteRatingClicked(object? sender, EventArgs e)
    {
        _viewModelPage.OnDeleteRatingClicked();
    }

    private void OnRatingClicked(object? sender, ItemTappedEventArgs e)
    {
        _viewModelPage.OnRatingClicked(e);
    }

    private void OnSaveEditRatingClicked(object? sender, EventArgs e)
    {
        _viewModelPage.OnSaveEditRatingClicked();
    }

    private async void OnSortActivityChanged(object? sender, EventArgs e)
    {
        if (sender is not Picker picker) return;
        
        var selectedValue = picker.SelectedItem.ToString();
        if (selectedValue != null) _viewModelPage.SelectedActivityOrderType = selectedValue;
        await _viewModelPage.OnSortActivityChanged();
    }

    private async void OnSortRatingChanged(object? sender, EventArgs e)
    {
        if (sender is not Picker picker) return;
        
        var selectedValue = picker.SelectedItem.ToString();
        if (selectedValue != null) _viewModelPage.SelectedRatingOrderType = selectedValue;
        await _viewModelPage.OnSortRatingChanged();
    }
}