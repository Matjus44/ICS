namespace ICS.BL.Models;

public record SubjectListModel : Modelbase
{
    public required string Name { get; set; }
    public required string CodeName { get; set; }
    
    public static SubjectListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CodeName = string.Empty
    };
}