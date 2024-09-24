using CarvedRock.Admin.Domain.Models;

namespace CarvedRock.Admin.Domain.Logic;
public interface ICategoryLogic
{
    Task<List<CategoryModel>> GetAllCategories();
    Task<CategoryModel?> GetCategoryById(int id);
    Task<CategoryModel?> GetCategoryById(int? id);
    Task<CategoryModel> AddNewCategory(CategoryModel CategoryToAdd);
    Task RemoveCategory(int id);
    Task UpdateCategory(CategoryModel CategoryToUpdate);
}
