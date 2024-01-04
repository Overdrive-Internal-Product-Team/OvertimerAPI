namespace ClockifyCloneAPI.Models.User;
public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool? Active { get; set; }
    public int? RoleId { get; set; }
    public int? CompanyId { get; set; }
}
