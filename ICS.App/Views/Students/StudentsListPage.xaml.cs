using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS.App.ViewModels.Students;

namespace ICS.App.Views.Students;

public partial class StudentsListPage
{
    private readonly StudentsViewListModel _viewListModel;
    
    public StudentsListPage(StudentsViewListModel viewListModel)
        : base(viewListModel)
    {
        _viewListModel = viewListModel;
        _viewListModel.SearchTermHandler();
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewListModel.SelectedSortOrder = string.Empty;
        await _viewListModel.LoadAsync();
    }
    
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        await _viewListModel.OnSearchTextChanged(e);
    }

    private async void OnItemSelected(object sender, ItemTappedEventArgs e)
    {
        await _viewListModel.OnItemSelected(e);
    }

    private async void CreateStudentClicked(object? sender, EventArgs eventArgs)
    {
        await _viewListModel.CreateStudentClicked();
    }

    private void OnSortOrderChanged(object? sender, EventArgs e)
    {
        var picker = sender as Picker;
        _viewListModel.SelectedSortOrder = picker?.SelectedItem.ToString() ?? string.Empty;
        _viewListModel.OnSortOrderChanged();
    }
}