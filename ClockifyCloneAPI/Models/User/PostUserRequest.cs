using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.User
{
    public class PostUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
    }
}
