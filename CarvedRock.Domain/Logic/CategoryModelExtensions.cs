using CarvedRock.Domain.Data;

namespace CarvedRock.Domain.Logic
{
    public static class CategoryModelExtensions
    {

        public static CategoryModel ToModel(this Category category)
        {
            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                MaxPrice = category.MaxPrice,
            };
        }
        public static Category ToCategory(this CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                MaxPrice = model.MaxPrice
            };

        }

    }
}
