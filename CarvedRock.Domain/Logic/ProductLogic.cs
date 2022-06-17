using CarvedRock.Domain.Data;
using FluentValidation;

namespace CarvedRock.Domain.Logic;

public class ProductLogic : IProductLogic
{
    private readonly ICarvedRockRepository _repo;
    private readonly IValidator<ProductModel> _validator;

    public ProductLogic(ICarvedRockRepository repo, IValidator<ProductModel> validator)
    {
        _repo = repo;
        _validator = validator;
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        var products = await _repo.GetAllProductsAsync();
        return products.Select(s => s.ToModel()).ToList();
    }

    public async Task<ProductModel?> GetProductById(int id)
    {
        var product = (await _repo.GetProductByIdAsync(id)).ToModel();

        if (product.Id == 0) return null;

        product.AvailableCategories = await GetAvailableCategoriesFromDb();
        return product ?? null;
    }

    public async Task AddNewProduct(ProductModel productToAdd)
    {
        await _validator.ValidateAndThrowAsync(productToAdd);
        var productToSave = productToAdd.ToProduct();
        await _repo.AddProductAsync(productToSave);
    }

    public async Task RemoveProduct(int id)
    {
        await _repo.RemoveProductAsync(id);
    }

    public async Task UpdateProduct(ProductModel productToUpdate)
    {
        var productToSave = productToUpdate.ToProduct();
        await _repo.UpdateProductAsync(productToSave);
    }

    public async Task<ProductModel> InitializeProductModel()
    {
        return new ProductModel { AvailableCategories = await GetAvailableCategoriesFromDb() };
    }

    private async Task<List<LookupModel>> GetAvailableCategoriesFromDb()
    {
        var cats = await _repo.GetAllCategoriesAsync();
        var returnList = new List<LookupModel> { new("None", string.Empty) };
        var availCatList = cats.Select(cat => new LookupModel(cat.Name, cat.Id.ToString()));
        returnList.AddRange(availCatList);
        return returnList;
    }

    public async Task GetAvailableCategories(ProductModel productModel)
    {
        productModel.AvailableCategories = await GetAvailableCategoriesFromDb();
    }
}
