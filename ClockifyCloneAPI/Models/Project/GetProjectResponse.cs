using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.Project;
public class GetProjectResponse : BaseModel
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Entities.Category Category { get; set; }
}

