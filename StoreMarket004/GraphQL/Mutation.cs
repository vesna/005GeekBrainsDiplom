using Contracts.Requests;
using StoreMarket004.BLL.Abstractions;

namespace StoreMarket004.GraphQL
{
    public class Mutation
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly IAuthService _authService;

        public Mutation(IProductService productService, ICategoryService categoryService, IStoreService storeService, IAuthService authService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _storeService = storeService;
            _authService = authService;
        }

        public int AddProduct(ProductCreateRequest request, string t) => _productService.AddProduct(request, t);
        public bool DeleteProduct(int id, string token) => _productService.DeleteProduct(id, token);
        public bool UpdateProductPrice(int id, decimal price, string token) => _productService.UpdateProductPrice(id, price, token);

        public int AddCategory(CategoryCreateRequest request, string token) => _categoryService.AddCategory(request, token);
        public bool DeleteCategory(int id, string token) => _categoryService.DeleteCategory(id, token);
        public bool UpdateCategoryName(int id, string name, string token) => _categoryService.UpdateCategoryName(id, name, token);

        public int AddStore(StoreCreateRequest store, string token) => _storeService.AddStore(store, token);
        public bool DeleteStore(int id, string token) => _storeService.DeleteStore(id, token);
        public bool AddProductToStore(ProductStoreCreateRequest request, string token) => _storeService.AddProductToStore(request, token);
        public bool DeleteProductFromStore(int storeId, int productId, string token) => _storeService.DeleteProductFromStore(storeId, productId, token);
        public bool UpdateStoreName(int id, string name, string token) => _storeService.UpdateStoreName(id, name, token);

        public string? Register(UserAuthRequest request) => _authService.Register(request);
    }
}
