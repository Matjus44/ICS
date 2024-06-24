using CommunityToolkit.Mvvm.Input;
using ICS.App.Services.Interfaces;
using ICS.App.ViewModels.Activities;
using ICS.App.ViewModels.Schedule;
using ICS.App.ViewModels.Students;
using ICS.App.ViewModels.Subjects;

namespace ICS.App;

public partial class AppShell
{
    private readonly INavigationService _navigationService;
    
    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }
    
    [RelayCommand]
    private async Task GoToExamsAsync()
        => await _navigationService.GoToAsync<ActivitiesViewModel>();
    
    [RelayCommand]
    private async Task GoToScheduleAsync()
        => await _navigationService.GoToAsync<ScheduleViewModel>();
    
    [RelayCommand]
    private async Task GoToSubjectsAsync()
        => await _navigationService.GoToAsync<SubjectsViewDetailModel>();
    
    [RelayCommand]
    private async Task GoToStudentsAsync()
        => await _navigationService.GoToAsync<StudentsViewListModel>();
    
}