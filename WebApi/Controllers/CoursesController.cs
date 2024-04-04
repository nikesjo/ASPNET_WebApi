using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
public class CoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    #region CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CourseDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _context.Courses.AnyAsync(x => x.Title == dto.Title))
            {
                try
                {
                    //_context.Courses.Add(dto);
                    await _context.SaveChangesAsync();
                    return Created("", null);
                }
                catch
                {
                    return Problem("Unable to create course.");
                }
            }

            return Conflict("The title already exists.");
        }

        return BadRequest();
    }
    #endregion

    #region READ

    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "")
    {
        var query = _context.Courses.Include(i => i.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(category) && category != "all")
            query = query.Where(x => x.Category!.CategoryName == category);

        if (!string.IsNullOrWhiteSpace(searchQuery))
            query = query.Where(x => x.Title.Contains(searchQuery) || x.Author.Contains(searchQuery));

        query = query.OrderByDescending(o => o.LastUpdated);

        var courses = await query.ToListAsync();

        var response = new CourseResult
        {
            Succeeded = true,
            Courses = CourseFactory.Create(courses)
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            return Ok(course);
        }

        return NotFound();
    }

    #endregion

    #region UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, CourseDto dto)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            try
            {
                //course = dto;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return Ok(course);
            }
            catch
            {
                return Problem("Unable to update course.");
            }
        }

        return NotFound();
    }
    #endregion

    #region DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }
    #endregion
}
