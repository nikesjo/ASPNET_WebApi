using Infrastructure.Dtos;

namespace Infrastructure.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Price { get; set; }
    public string? DiscountPrice { get; set; }
    public string? Hours { get; set; }
    public bool IsBestSeller { get; set; }
    public string? LikesInNumbers { get; set; }
    public string? LikesInProcent { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? Preamble { get; set; }
    public string? Description { get; set; }
    public string? Learn { get; set; }
    public string? ProgramDetails { get; set; }

    public static implicit operator CourseEntity(CourseDto dto)
    {
        return new CourseEntity
        {
            Title = dto.Title,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            Hours = dto.Hours,
            IsBestSeller = dto.IsBestSeller,
            LikesInNumbers = dto.LikesInNumbers,
            LikesInProcent = dto.LikesInProcent,
            Author = dto.Author,
            ImageUrl = dto.ImageUrl,
            AuthorImageUrl = dto.AuthorImageUrl,
            Preamble = dto.Preamble,
            Description = dto.Description,
            Learn = dto.Learn,
            ProgramDetails = dto.ProgramDetails
        };
    }
}
