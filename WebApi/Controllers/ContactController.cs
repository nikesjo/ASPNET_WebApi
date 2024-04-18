using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
//[Authorize]
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
                var contactEntity = ContactFactory.Create(dto);
                _context.Contacts.Add(contactEntity);
                await _context.SaveChangesAsync();
                return Created("", null);
            }
            catch
            {
                return Problem("Unable to send message.");
            }
        }

        return BadRequest();
    }
}
