using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Admin.Domain.Models;

public class LookupModel
{
    public LookupModel(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Value { get; set; }
    public string Name { get; set; }
}

public class ProductModel
{


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
    public List<LookupModel> AvailableCategories { get; set; } = new();
}