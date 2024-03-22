using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class ContactEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    public string? ServiceOption { get; set; }

    [Required]
    public string Message { get; set; } = null!;

    public static implicit operator ContactEntity(ContactDto dto)
    {
        return new ContactEntity
        {
            FullName = dto.FullName,
            Email = dto.Email,
            ServiceOption = dto.ServiceOption,
            Message = dto.Message
        };
    }
}
