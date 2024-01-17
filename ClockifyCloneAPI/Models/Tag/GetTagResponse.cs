using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.Tag;
public class GetTagResponse : BaseModel
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
}

