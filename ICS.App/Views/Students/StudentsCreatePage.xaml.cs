using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS.App.ViewModels.Students;

namespace ICS.App.Views.Students;

public partial class StudentsCreatePage
{
    private readonly StudentsViewCreateModel _viewCreateModel;
    
    public StudentsCreatePage(StudentsViewCreateModel viewCreateModel)
        : base(viewCreateModel)
    {
        _viewCreateModel = viewCreateModel;
        InitializeComponent();
    }

    private async void SaveStudentClicked(object? sender, EventArgs e)
    {
        await _viewCreateModel.SaveStudentClicked();
    }
}