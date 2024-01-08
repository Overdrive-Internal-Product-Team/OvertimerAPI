namespace ClockifyCloneAPI.Models.Category;
public class GetCategoryResponse : BaseModel
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public List<Entities.Project> Projects { get; set; }
}
