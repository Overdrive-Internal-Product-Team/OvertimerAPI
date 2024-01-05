namespace ClockifyCloneAPI.Entities;
public class Tag : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public List<Work> Works { get; set; }
}
