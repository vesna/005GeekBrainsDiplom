using Contracts.Requests;
using Contracts.Responses;
using StoreMarket004.BLL.Abstractions;

namespace StoreMarket004.GraphQL
{
    public class Query
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly IAuthService _authService;

        public Query(IProductService productService, ICategoryService categoryService, IStoreService storeService, IAuthService authService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _storeService = storeService;
            _authService = authService;
        }

        public ProductResponse? GetProduct(int id) => _productService.GetProductById(id);
        public IEnumerable<ProductResponse> GetProducts() => _productService.GetProducts();

        public CategoryResponse? GetCategory(int id) => _categoryService.GetCategoryById(id);
        public IEnumerable<CategoryResponse> GetCategories() => _categoryService.GetCategories();

        public IEnumerable<StoreResponse> GetStores() => _storeService.GetStores();
        public StoreResponse? GetStoreById(int storeId) => _storeService.GetStoreById(storeId);

        public string? Login(string email, string password) => _authService.Login(email, password);
    }
}
