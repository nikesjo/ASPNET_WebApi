using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories;

public class CourseFactory
{
    public static CourseDto Create(CourseEntity entity)
    {
        try
        {
            return new CourseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Author = entity.Author,
                OriginalPrice = entity.OriginalPrice,
                DiscountPrice = entity.DiscountPrice,
                Hours = entity.Hours,
                IsBestSeller = entity.IsBestSeller,
                LikesInProcent = entity.LikesInProcent,
                LikesInNumbers = entity.LikesInNumbers,
                ImageUrl = entity.ImageUrl,
                AuthorImageUrl = entity.AuthorImageUrl,
                Category = entity.Category!.CategoryName
            };
        }
        catch { }
        return null!;
    }

    public static IEnumerable<CourseDto> Create(List<CourseEntity> entities)
    {
        List<CourseDto> courses = [];

        try
        {
            foreach (var entity in entities)
                courses.Add(Create(entity));
        }
        catch { }
        return courses;
    }
}
