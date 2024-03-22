using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Contact(ContactDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Contact.Add(dto);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return Problem("Unable to send message.");
            }
        }

        return BadRequest();
    }
}
