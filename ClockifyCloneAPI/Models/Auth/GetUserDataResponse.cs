using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.Auth
{
    public class GetUserDataResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool active { get; set; }
        public RoleEntity Role { get; set; }
        public CompanyEntity Company { get; set; }

    }
}
