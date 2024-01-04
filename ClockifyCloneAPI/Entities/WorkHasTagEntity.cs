namespace ClockifyCloneAPI.Entities;
public class WorkHasTagEntity : BaseEntity
{
    public int WorkId { get; set; }
    public WorkEntity Work { get; set; }
    public int TagId { get; set; }
    public TagEntity Tag { get; set; }
}
