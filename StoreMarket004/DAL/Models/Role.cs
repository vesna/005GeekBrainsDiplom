using System.ComponentModel.DataAnnotations;

namespace StoreMarket004.DAL.Models
{
    public class Role
    {
        [Key]public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
