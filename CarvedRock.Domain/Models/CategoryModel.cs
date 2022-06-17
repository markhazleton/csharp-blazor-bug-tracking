using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Domain.Models;
public class CategoryModel
{
    [Required]
    public int Id { get; set; }
    public bool IsActive { get; set; }
    [DisplayName("Category Name")]
    public string Name { get; set; } = null!;
    public decimal MaxPrice { get; set; } = 500.00M;


}
