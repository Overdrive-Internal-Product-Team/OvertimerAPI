using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.Tag;
public class GetAllTagResponse : BaseModel
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
}

