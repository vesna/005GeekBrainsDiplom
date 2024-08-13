using Contracts.Requests;
using Contracts.Responses;

namespace StoreMarket004.BLL.Abstractions
{
    public interface IStoreService
    {
        public int AddStore(StoreCreateRequest store, string token);

        public IEnumerable<StoreResponse> GetStores();

        public StoreResponse? GetStoreById(int storeId);

        public bool DeleteStore(int id, string token);

        public bool AddProductToStore(ProductStoreCreateRequest request, string token);

        public bool DeleteProductFromStore(int storeId, int productId, string token);

        public bool UpdateStoreName(int id, string name, string token);

        // public CategoryResponse? GetCategoryByName(string name);
    }
}
