using ICS.App.Models;
using ICS.App.Services.Interfaces;
using ICS.App.ViewModels;
using ICS.App.ViewModels.Activities;
using ICS.App.ViewModels.Schedule;
using ICS.App.ViewModels.Students;
using ICS.App.ViewModels.Subjects;
using ICS.App.Views.Activities;
using ICS.App.Views.Schedule;
using ICS.App.Views.Students;
using ICS.App.Views.Subjects;

namespace ICS.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//activities", typeof(ActivitiesPage), typeof(ActivitiesViewModel)),
        new("//schedule", typeof(SchedulePage), typeof(ScheduleViewModel)),
        new("//subjects", typeof(SubjectsPage), typeof(SubjectsViewDetailModel)),
        new("//subjects/edit", typeof(SubjectsEditPage), typeof(SubjectsEditDetailModel)),
        new("//studentsList", typeof(StudentsListPage), typeof(StudentsViewListModel)),
        new("//studentsCreate", typeof(StudentsCreatePage), typeof(StudentsViewCreateModel)),
        new("//studentsDetail", typeof(StudentsDetailPage), typeof(StudentsViewDetailModel)),
        new("//studentsEdit", typeof(StudentsEditPage), typeof(StudentsViewEditModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel 
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}