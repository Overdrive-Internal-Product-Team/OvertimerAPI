namespace ClockifyCloneAPI.Entities;
public class Work : BaseEntity
{
    public string Title { get; set; }    
    public DateTime InitialDateTime { get; set; }
    public DateTime FinalDateTime { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public List<Tag> Tags { get; set; }
}
