namespace Contracts.Requests
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public int? CategoryId { get; set; }
        
    }
}
