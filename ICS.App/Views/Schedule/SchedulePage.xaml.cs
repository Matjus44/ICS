using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Maui.Controls;
using ICS.App.ViewModels.Schedule;
using ICS.BL.Facades.Interfaces;
using ICS.BL.Models;
using Label = Microsoft.Maui.Controls.Label;

namespace ICS.App.Views.Schedule;

public partial class SchedulePage
{
    private readonly ScheduleViewModel _viewModelPage;
    public SchedulePage(ScheduleViewModel viewModelPage)
        : base(viewModelPage)
    {
        _viewModelPage = viewModelPage;
        _viewModelPage.SearchTermHandler();
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        ScheduleGrid = this.FindByName<Grid>("ScheduleGrid");
        await _viewModelPage.LoadStudents();
        await _viewModelPage.GetRegisteredSubjects();
        await _viewModelPage.GetAllActivities();
        AssignColorsToSubjects();
        DisplayActivitiesInScheduleGrid();
    }

    private readonly Dictionary<string, Color> _subjectColors = new Dictionary<string, Color>();

    private void DisplayActivitiesInScheduleGrid()
    {
        foreach (var activity in _viewModelPage.Activities)
        {
            var row = GetRow(activity);
            var startColumn = GetStartColumn(activity);
            var endColumn = GetEndColumn(activity);

            var subject = _viewModelPage.GetSubjectDetailById(activity.SubjectId).Result;

            var frame = new Frame
            {
                Content = new Label
                {
                    Text = subject.CodeName + "\n" + activity.Room + "\n" + activity.Type,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                },
                BackgroundColor = GetColorForSubject(subject.Name),
                BorderColor = Colors.Black,
                HasShadow = false,
                Padding = new Thickness(1),
                AutomationId = "DynamicLabel"
            };
            
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, startColumn);
            Grid.SetColumnSpan(frame, endColumn - startColumn);

            ScheduleGrid.Children.Add(frame);
        }
    }
    
    
    private void AssignColorsToSubjects()
    {
        _subjectColors.Clear();

        Color[] predefinedColors = [Colors.CornflowerBlue, Colors.LightCoral, Colors.PaleGreen, Colors.SandyBrown, Colors.DarkViolet
        ];
        var index = 0;
        foreach (var subject in _viewModelPage.RegisteredSubjects)
        {
            if (_subjectColors.ContainsKey(subject.Name)) continue;
            _subjectColors.Add(subject.Name, predefinedColors[index % predefinedColors.Length]);
            index++;
        }
    }

    private Color GetColorForSubject(string subjectName)
    {
        if (_subjectColors.ContainsKey(subjectName))
        {
            return _subjectColors[subjectName];
        }
        return Colors.Gray;
    }

    private void RemoveAllActivitiesFromGrid()
    {
        var dynamicActivities = ScheduleGrid.Children.Where(child => child is Frame && ((Frame)child).AutomationId?.StartsWith("DynamicLabel") == true).ToList();
        foreach (var frame in dynamicActivities)
        {
            ScheduleGrid.Children.Remove(frame);
        }
    }

    private async void OnStudentSelected(object sender, ItemTappedEventArgs e)
    {
        _viewModelPage.OnStudentSelected(e);
        RemoveAllActivitiesFromGrid();
        await _viewModelPage.GetAllActivities();
        AssignColorsToSubjects();
        DisplayActivitiesInScheduleGrid();
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModelPage.OnSearchTextChanged(e);
    }

    private async void OnFilterButtonClicked(object? sender, EventArgs e)
    {
        RemoveAllActivitiesFromGrid();
        await _viewModelPage.GetFilteredActivities();
        AssignColorsToSubjects();
        DisplayActivitiesInScheduleGrid();
    }

    private void OnSortStudentsChanged(object? sender, EventArgs e)
    {
        var picker = sender as Picker;
        _viewModelPage.SelectedSortOrder = picker?.SelectedItem.ToString() ?? string.Empty;
        _viewModelPage.OnSortOrderChanged();
    }
}