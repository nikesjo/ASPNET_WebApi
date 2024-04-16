using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class CoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    #region CREATE
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CourseDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == dto.Category);

                if (category != null)
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                }

                _context.Courses.Add(dto);
                await _context.SaveChangesAsync();
                return Created("", null);
            }
            catch
            {
                return Problem("Unable to create course.");
            }
        }

        return BadRequest();
    }
    #endregion

    #region READ

    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            #region query filters

            var query = _context.Courses.Include(i => i.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != "all")
                query = query.Where(x => x.Category!.CategoryName == category);

            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(x => x.Title.Contains(searchQuery) || x.Author.Contains(searchQuery));

            query = query.OrderByDescending(o => o.LastUpdated);

            #endregion

            var response = new CourseResult
            {
                Succeeded = true,
                TotalItems = await query.CountAsync()
            };
            response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
            response.Courses = CourseFactory.Create(await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());

            return Ok(response);
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
            {
                return Ok(course);
            }
        }
        catch
        {
            return NotFound();
        }
        
        return BadRequest();
    }

    #endregion

    #region UPDATE
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, CourseDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
                if (course != null)
                {
                    course = dto;
                    _context.Courses.Update(course);
                    await _context.SaveChangesAsync();
                    return Ok(course);
                }

                return NotFound();       
            }
            catch
            {
                return Problem("Unable to update course.");
            }
        }

        return BadRequest();
    }
    #endregion

    #region DELETE
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        if (ModelState.IsValid)
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

        return BadRequest();
    }
    #endregion
}
