namespace ClockifyCloneAPI.Entities;
public class ProjectEntity : BaseEntity
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}
