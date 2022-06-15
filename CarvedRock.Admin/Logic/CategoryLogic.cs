namespace CarvedRock.Admin.Logic;

public class CategoryLogic : ICategoryLogic
{
    private readonly ICarvedRockRepository _repo;

    public CategoryLogic(ICarvedRockRepository repo)
    {
        _repo = repo;
    }

    public async Task<CategoryModel> AddNewCategory(CategoryModel CategoryToAdd)
    {
        var categoryToSave = CategoryToAdd.ToCategory();
        categoryToSave = await _repo.AddCategoryAsync(categoryToSave);
        return CategoryModel.FromCategory(categoryToSave);
    }

    public async Task<List<CategoryModel>> GetAllCategories()
    {
        var cats = await _repo.GetAllCategoriesAsync();
        return cats.Select(CategoryModel.FromCategory).ToList();
    }

    public async Task<CategoryModel?> GetCategoryById(int? id)
    {
        if (id == null) return null;
        var category = await _repo.GetCategoryByIdAsync(id.Value);
        return category == null ? null : CategoryModel.FromCategory(category);
    }

    public async Task<CategoryModel?> GetCategoryById(int id)
    {
        if (id == 0) return null;
        var category = await _repo.GetCategoryByIdAsync(id);
        return category == null ? null : CategoryModel.FromCategory(category);
    }

    public async Task RemoveCategory(int id)
    {
        await _repo.RemoveCategoryAsync(id);
    }

    public async Task UpdateCategory(CategoryModel categoryToUpdate)
    {
        var categoryToSave = categoryToUpdate.ToCategory();
        await _repo.UpdateCategoryAsync(categoryToSave);
    }
}