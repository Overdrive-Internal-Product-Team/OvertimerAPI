
namespace ClockifyCloneAPI.Models.Work;
public class GetAllWorkResponse : BaseModel
{
    public string Title { get; set; }
    public DateTime InitialDateTime { get; set; }
    public DateTime FinalDateTime { get; set; }
    public int ProjectId { get; set; }
    public Entities.Project Project { get; set; }
    public List<Entities.Tag> Tags { get; set; }
}
