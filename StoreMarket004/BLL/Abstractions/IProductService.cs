using Contracts.Requests;
using Contracts.Responses;

namespace StoreMarket004.BLL.Abstractions
{
    public interface IProductService
    {
        public int AddProduct(ProductCreateRequest product, string token);

        public IEnumerable<ProductResponse> GetProducts();

        public ProductResponse? GetProductById(int productId);

        public bool DeleteProduct(int id, string token);

        public bool UpdateProductPrice(int id, decimal price, string token);

        // public ProductResponse? GetProductByName(string name);
    }
}
