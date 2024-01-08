namespace ClockifyCloneAPI.Models.Work;
public class UpdateWorkRequest
{
    public string? Title { get; set; }
    public DateTime? InitialDateTime { get; set; }
    public DateTime? FinalDateTime { get; set; }
    public int? UserId { get; set; }
    public int? ProjectId { get; set; }
    public List<int>? TagIds { get; set; }
}
