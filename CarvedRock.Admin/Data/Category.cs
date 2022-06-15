using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Admin.Data;
public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public decimal MaxPrice { get; set; } = 500.00M;
}