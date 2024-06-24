using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS.App.ViewModels.Students;

namespace ICS.App.Views.Students;

public partial class StudentsEditPage
{
    private readonly StudentsViewEditModel _viewEditModel;
    
    public StudentsEditPage(StudentsViewEditModel viewEditModel)
        : base(viewEditModel)
    {
        _viewEditModel = viewEditModel;
        _viewEditModel.LoadStudent();
        InitializeComponent();
    }

    private async void SaveStudentAsync(object? sender, EventArgs e)
    {
        await _viewEditModel.SaveStudentAsync();
    }
}