using ClockifyCloneAPI.Entities;
namespace ClockifyCloneAPI.Models.Category;
public class GetAllCategoryResponse : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
}
