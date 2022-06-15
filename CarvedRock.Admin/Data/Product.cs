using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Admin.Data;
public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}
