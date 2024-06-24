namespace ICS.DAL.Entities;

public record StudentEntity : IEntity
{
    public Guid Id { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Uri? PhotoUri { get; set; }
    public List<StudentsSubjectsEntity> RegisteredSubjects { get; set; } = new List<StudentsSubjectsEntity>();
}