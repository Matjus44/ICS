using System.Collections.ObjectModel;

namespace ICS.BL.Models;

public record SubjectDetailModel : Modelbase
{
    public required string Name { get; set; }
    public required string CodeName { get; set; }
    public ObservableCollection<StudentsSubjectsListModel> RegisteredStudents { get; init; } = new();
    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
    
    public static SubjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CodeName = string.Empty
    };
}