using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests
{
    public class UserAuthRequest
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = RoleType.User.ToString();
    }
}
