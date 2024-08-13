using System.ComponentModel.DataAnnotations;

namespace StoreMarket004.DAL.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Store> Store { get; } = [];
        public virtual List<ProductStore> ProductStore { get; } = [];
    }
}
