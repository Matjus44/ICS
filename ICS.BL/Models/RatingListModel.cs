namespace ICS.BL.Models;

public record RatingListModel : Modelbase
{
    public Guid StudentId { get; init; }
    public Guid ActivityId { get; init; }
    public int Rating { get; init; }
    public required string Note { get; init; }

    public static RatingListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        StudentId = Guid.Empty,
        ActivityId = Guid.Empty,
        Rating = 0,
        Note = string.Empty
    };
}