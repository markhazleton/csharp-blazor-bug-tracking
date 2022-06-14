using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Admin.Models;

public class ProductModel
{
    public static ProductModel FromProduct(Product product)
    {
        return new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId ?? 0,
            CategoryName = product.Category?.Name

        };
    }
    public Product ToProduct()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            IsActive = IsActive,
            CategoryId = CategoryId
        };

    }

    [Required]
    public string Description { get; set; } = null!;
    public int Id { get; set; }
    public bool IsActive { get; set; }
    [Required]
    [DisplayName("Product Name")]
    public string Name { get; set; } = null!;
    [DataType(DataType.Currency)]
    [Range(0.01, 1000.00, ErrorMessage = "Value for {0} must be between " + "{1:C} and {2:C}")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    [DisplayName("Category")]
    public string? CategoryName { get; set; }
    public List<SelectListItem> AvailableCategories { get; set; } = new();
}