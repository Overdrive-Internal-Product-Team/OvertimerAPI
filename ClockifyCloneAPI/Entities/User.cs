namespace ClockifyCloneAPI.Entities;
public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int CompanyId { get; set; }    
    public Company Company { get; set; }

}
