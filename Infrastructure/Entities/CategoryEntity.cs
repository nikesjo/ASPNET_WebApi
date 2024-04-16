using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CategoryEntity
{
    [Key]
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;


    public static implicit operator CategoryEntity(CategoryDto dto)
    {
        return new CategoryEntity
        {
            Id = dto.Id,
            CategoryName = dto.CategoryName
        };
    }
}
