using ICS.App.ViewModels.Students;

namespace ICS.App.Views.Students;

public partial class StudentsDetailPage
{
    private readonly StudentsViewDetailModel _viewDetailModel;
    
    public StudentsDetailPage(StudentsViewDetailModel viewDetailModel)
        : base(viewDetailModel)
    {
        _viewDetailModel = viewDetailModel;
        _viewDetailModel.LoadStudent();
        InitializeComponent();
    }

    private void EditStudentClicked(object? sender, EventArgs e)
    {
        _viewDetailModel.EditStudent();
    }

    private void DeleteStudentClicked(object? sender, EventArgs e)
    {
        _viewDetailModel.DeleteStudent();
    }
}