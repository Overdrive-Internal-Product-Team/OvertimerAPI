namespace ClockifyCloneAPI.Entities;
public class WorkHasTag : BaseEntity
{
    public int WorkId { get; set; }
    public Work Work { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
