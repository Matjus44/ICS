using System.Collections.ObjectModel;
using ICS.Common.Enums;
using ICS.DAL.Entities;

namespace ICS.BL.Models;

public record ActivityDetailModel : Modelbase
{
    public Guid SubjectId { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public RoomName Room { get; set; }
    public ActivityType Type { get; set; }
    public required string ActivityDescription { get; set; }
    public ObservableCollection<RatingListModel> Ratings { get; set; } = new();

    public static ActivityDetailModel Empty => new()
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