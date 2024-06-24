using System.Collections.ObjectModel;

namespace ICS.BL.Models;

public record StudentDetailModel : Modelbase
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Uri? PhotoUri { get; set; }
    public ObservableCollection<SubjectListModel> RegisteredSubjects { get; init; } = new();

    public static StudentDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        FirstName = string.Empty,
        LastName = string.Empty,
        PhotoUri = null
        
    };
}