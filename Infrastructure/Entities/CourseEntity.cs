using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string OriginalPrice { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    public int Hours { get; set; }
    public bool IsBestSeller { get; set; }
    public string? LikesInProcent { get; set; }
    public string? LikesInNumbers { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? ImageUrl { get; set; }
    public string? AuthorImageUrl { get; set; }

    public int? CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }


    public static implicit operator CourseEntity(CourseDto dto)
    {
        return new CourseEntity
        {
            Title = dto.Title,
            Author = dto.Author,
            OriginalPrice = dto.OriginalPrice,
            DiscountPrice = dto.DiscountPrice,
            Hours = dto.Hours,
            LikesInNumbers = dto.LikesInNumbers,
            LikesInProcent = dto.LikesInProcent,
            IsBestSeller = dto.IsBestSeller,
            ImageUrl = dto.ImageUrl,
            AuthorImageUrl = dto.AuthorImageUrl,
            Created = DateTime.Now,
            LastUpdated = DateTime.Now,
            Category = new CategoryEntity
            {
                CategoryName = dto.Category
            }
        };
    }
}
