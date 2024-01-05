namespace ClockifyCloneAPI.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public List<Project> Projects { get; set; }
    public Company Company { get; set; }
}
