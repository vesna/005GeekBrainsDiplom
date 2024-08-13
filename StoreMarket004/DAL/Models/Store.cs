using System.ComponentModel.DataAnnotations;

namespace StoreMarket004.DAL.Models
{
    public class Store
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public int Count { get; set; }
        public virtual List<Product> Product { get; } = [];
        public virtual List<ProductStore> ProductStore { get; } = [];
    }
}
