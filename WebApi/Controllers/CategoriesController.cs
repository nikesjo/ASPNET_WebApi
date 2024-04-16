using Infrastructure.Contexts;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _context.Categories.OrderBy(o => o.CategoryName).ToListAsync();
                return Ok(CategoryFactory.Create(categories));
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

            return BadRequest();
        }
    }
}
