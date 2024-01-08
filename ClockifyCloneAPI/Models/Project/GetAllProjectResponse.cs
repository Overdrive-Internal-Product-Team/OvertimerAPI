using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.Project;
public class GetAllProjectResponse : BaseModel
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
}

