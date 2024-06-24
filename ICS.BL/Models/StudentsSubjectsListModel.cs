namespace ICS.BL.Models;

public record StudentsSubjectsListModel : Modelbase
{
    public Guid StudentId { get; init; }
    public Guid SubjectId { get; init; }

    public static StudentsSubjectsListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        StudentId = Guid.Empty,
        SubjectId = Guid.Empty
    };
}

