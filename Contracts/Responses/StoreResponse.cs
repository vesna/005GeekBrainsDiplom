namespace Contracts.Responses
{
    public class StoreResponse
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
    }
}
