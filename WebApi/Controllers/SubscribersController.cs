using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
public class SubscribersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscriberDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _context.Subscribers.AnyAsync(x => x.Email == dto.Email))
            {
                try
                {
                    //_context.Subscribers.Add(dto);
                    await _context.SaveChangesAsync();
                    return Created("", null);
                }
                catch
                {
                    return Problem("Unable to create subscription.");
                }
            }

            return Conflict("Your email address is already subscribed.");
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscriber(string id)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        if (subscriber != null)
        {
            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }
}
