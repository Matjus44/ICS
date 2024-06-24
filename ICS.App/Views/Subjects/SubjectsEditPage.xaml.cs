
using ICS.App.ViewModels.Subjects;
using ICS.BL.Models;


namespace ICS.App.Views.Subjects;

public partial class SubjectsEditPage
{
    private readonly SubjectsEditDetailModel _editDetailModelPage;

    public SubjectsEditPage(SubjectsEditDetailModel editDetailModelPage)
        : base(editDetailModelPage)
    {
        _editDetailModelPage = editDetailModelPage;
        InitializeComponent();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _editDetailModelPage.LoadStudentsFromSubject();
        await _editDetailModelPage.LoadActivitiesFromSubject();
        _editDetailModelPage.LoadAllStudents();
    }
    // private void Button_OnClicked(object? sender, EventArgs e)
    // {
    //     Task.Run((() => _editDetailModelPage.LoadStudents()));
    // }
    private void OnRegisterStudentClicked(object? sender, EventArgs e)
    {
        _editDetailModelPage.OnRegisterStudentClicked();
    }

    private async void OnAddActivityClicked(object? sender, EventArgs e)
    {
        await _editDetailModelPage.OnActivityAddClicked();
    }

    private void OnDeleteActivityClicked(object? sender, EventArgs e)
    {
        _editDetailModelPage.OnActivityDeleteClicked();
    }

    private async void OnUnregisterStudentClicked(object? sender, EventArgs e)
    {
        await _editDetailModelPage.OnUnregisterStudentClicked();
    }

    private void OnStudentSelected(object? sender, ItemTappedEventArgs e)
    {
        _editDetailModelPage.OnStudentSelected(e);
    }

    private void OnStudentToRegisterSelected(object? sender, ItemTappedEventArgs e)
    {
        _editDetailModelPage.OnStudentToRegisterClicked(e);
    }

    private void OnActivitySelected(object? sender, ItemTappedEventArgs e)
    {
        _editDetailModelPage.OnActivitySelected(e);
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        _editDetailModelPage.OnSaveClicked();
    }

    private void OnEditActivityClicked(object? sender, EventArgs e)
    {
        _editDetailModelPage.OnEditActivityClicked();
    }

    private void OnSaveActivityClicked(object? sender, EventArgs e)
    {
        _editDetailModelPage.OnSaveActivityClicked();
    }
}