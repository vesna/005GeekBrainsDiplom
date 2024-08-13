namespace Contracts.Requests
{
    public class ProductCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public int? CategoryId { get; set; }
    }
}
