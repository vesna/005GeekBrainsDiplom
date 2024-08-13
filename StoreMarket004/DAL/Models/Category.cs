using System.ComponentModel.DataAnnotations;

namespace StoreMarket004.DAL.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
