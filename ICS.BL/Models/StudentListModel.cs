using System.Collections.ObjectModel;

namespace ICS.BL.Models;

public record StudentListModel : Modelbase
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public Uri? PhotoUri { get; set; }
    
    public ObservableCollection<SubjectListModel> RegisteredSubjects { get; init; } = new();

    public static StudentListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        FirstName = string.Empty,
        LastName = string.Empty
    };
}