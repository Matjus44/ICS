using ICS.Common.Enums;
namespace ICS.BL.Models;

public record ActivityListModel : Modelbase
{
    public Guid SubjectId { get; init; }
    public DateTime Start { get; init; }
    public DateTime Finish { get; init; }
    public RoomName Room { get; init; }
    public ActivityType Type { get; init; }
    public required string ActivityDescription { get; init; }

    public static ActivityListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        SubjectId = Guid.Empty,
        Start = DateTime.MinValue,
        Finish = DateTime.MinValue,
        Room = RoomName.None,
        Type = ActivityType.None,
        ActivityDescription = string.Empty
    };
}