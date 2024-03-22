using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class ContactDto
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    public string? ServiceOption { get; set; }

    [Required]
    public string Message { get; set; } = null!;
}
