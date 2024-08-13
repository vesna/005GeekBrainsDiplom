using System.ComponentModel.DataAnnotations;

namespace StoreMarket004.DAL.Models
{
    public class User
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required byte[] Password { get; set; }
        public required byte[] Salt { get; set; }
        public Guid RoleId { get; set; }
        //public virtual Role? Role { get; set; }
    }
}
