using ICS.DAL.Entities;

namespace ICS.BL.Models;

public record StudentsSubjectsDetailModel : Modelbase
{
    public Guid StudentId { get; init; }
    public Guid SubjectId { get; init; }
    public required StudentListModel Student { get; init; }
    public required SubjectListModel Subject { get; init; }

    public static StudentsSubjectsDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        StudentId = Guid.Empty,
        SubjectId = Guid.Empty,
        Student = StudentListModel.Empty,
        Subject = SubjectListModel.Empty
    };
}
