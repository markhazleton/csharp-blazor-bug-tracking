using CarvedRock.Admin.Domain.Data;
using CarvedRock.Admin.Domain.Models;

namespace CarvedRock.Admin.Domain.Logic
{
    public static class ProductModelExtensions
    {
        public static ProductModel ToModel(this Product? product)
        {
            if (product == null) return new ProductModel();

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsActive = product.IsActive,
                Description = product.Description,
                CategoryId = product.Category?.Id ?? 0,
                CategoryName = product.Category?.Name ?? string.Empty,
                AvailableCategories = new List<LookupModel>()
            };

        }
        public static Product ToProduct(this ProductModel model)
        {
            return new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
                Price = model.Price,
                CategoryId = model.CategoryId
            };
        }
    }
}
