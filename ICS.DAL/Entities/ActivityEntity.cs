using ICS.Common.Enums;

namespace ICS.DAL.Entities;

public record ActivityEntity: IEntity
{
    public Guid Id { get; init; }
    public required DateTime Start { get; set; }
    public required DateTime Finish { get; set; }
    public required RoomName Room { get; set; }
    public required ActivityType Type { get; set; }
    public required string ActivityDescription { get; set; }
    public Guid SubjectId { get; init; }
    public SubjectEntity Subject { get; set; } = null!;
    public ICollection<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
}