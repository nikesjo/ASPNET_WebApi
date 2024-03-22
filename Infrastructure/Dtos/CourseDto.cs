using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class CourseDto
{
    [Required]
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
}
