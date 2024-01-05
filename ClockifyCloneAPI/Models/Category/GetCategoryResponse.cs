using ClockifyCloneAPI.Entities;
namespace ClockifyCloneAPI.Models.Category;
public class GetCategoryResponse : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public List<Project> Projects { get; set; }
}
