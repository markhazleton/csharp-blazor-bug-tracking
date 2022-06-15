﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Admin.Models;
public class CategoryModel
{
    [Required]
    public int Id { get; set; }
    public bool IsActive { get; set; }
    [DisplayName("Category Name")]
    public string Name { get; set; } = null!;
    public decimal MaxPrice { get; set; } = 500.00M;

    public static CategoryModel FromCategory(Category category)
    {
        return new CategoryModel
        {
            Id = category.Id,
            Name = category.Name,
            MaxPrice = category.MaxPrice,
        };
    }
    public Category ToCategory()
    {
        return new Category
        {
            Id = Id,
            Name = Name,
            MaxPrice = MaxPrice
        };

    }
}
