namespace Contracts.Requests
{
    public class StoreCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public int? Count { get; set; }
    }
}
