namespace ClockifyCloneAPI.Entities;
public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public List<ProjectEntity> Projects { get; set; }
    public CompanyEntity Company { get; set; }
}
