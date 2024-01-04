namespace ClockifyCloneAPI.Entities;
public class WorkEntity : BaseEntity
{
    public string Title { get; set; }    
    public DateTime InitialDateTime { get; set; }
    public DateTime FinalDateTime { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
    public List<TagEntity> Tags { get; set; }
}
