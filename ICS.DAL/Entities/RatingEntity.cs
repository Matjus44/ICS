namespace ICS.DAL.Entities;

public record RatingEntity : IEntity
{
    public Guid Id { get; init; }
    public required int Rating { get; set; }
    public required string Note { get; set; }

    public Guid StudentId { get; init; }
    public StudentEntity Student { get; init; } = null!;

    public Guid ActivityId { get; init; }
    public ActivityEntity Activity { get; init; } = null!;
}