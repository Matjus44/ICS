namespace ICS.DAL.Entities;

public record StudentsSubjectsEntity : IEntity
{
    public Guid Id { get; init; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }

    public StudentEntity Student { get; set; } = null!;
    public SubjectEntity Subject { get; set; } = null!;
}