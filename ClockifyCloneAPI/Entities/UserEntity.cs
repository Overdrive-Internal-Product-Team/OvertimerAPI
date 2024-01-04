namespace ClockifyCloneAPI.Entities;
public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool active { get; set; }
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; }
    public int CompanyId { get; set; }    
    public CompanyEntity Company { get; set; }

}
