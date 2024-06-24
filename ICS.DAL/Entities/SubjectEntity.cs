namespace ICS.DAL.Entities;

public record SubjectEntity : IEntity
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required string CodeName { get; set; }
    public ICollection<StudentsSubjectsEntity> RegisteredStudents { get; set; } = new List<StudentsSubjectsEntity>();
    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
}