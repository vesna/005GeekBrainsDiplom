using Contracts.Requests;
using Contracts.Responses;

namespace StoreMarket004.BLL.Abstractions
{
    public interface ICategoryService
    {
        public int AddCategory(CategoryCreateRequest category, string token);

        public IEnumerable<CategoryResponse> GetCategories();

        public CategoryResponse? GetCategoryById(int categoryId);

        public bool DeleteCategory(int id, string token);

        public bool UpdateCategoryName(int id, string name, string token);

        // public CategoryResponse? GetCategoryByName(string name);
    }
}