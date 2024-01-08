namespace ClockifyCloneAPI.Models.Work;
public class GetWorkResponse : BaseModel
{
    public string Title { get; set; }
    public DateTime InitialDateTime { get; set; }
    public DateTime FinalDateTime { get; set; }
    public int UserId { get; set; }
    public Entities.User User { get; set; }
    public int ProjectId { get; set; }
    public Entities.Project Project { get; set; }
    public List<Entities.Tag> Tags { get; set; }
}

