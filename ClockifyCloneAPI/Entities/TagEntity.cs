namespace ClockifyCloneAPI.Entities;
public class TagEntity : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; }
    public List<WorkEntity> Works { get; set; }
}
