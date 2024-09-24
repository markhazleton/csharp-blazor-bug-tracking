
namespace CarvedRock.Admin.Domain.Data;
public interface ICarvedRockRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task RemoveProductAsync(int productIdToRemove);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task<Category> AddCategoryAsync(Category category);
    Task RemoveCategoryAsync(int id);
    Task UpdateCategoryAsync(Category categoryToSave);
}