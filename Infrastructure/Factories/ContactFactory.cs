using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories;

public class ContactFactory
{
    public static ContactEntity Create(ContactDto dto)
    {
        try
        {
            return new ContactEntity
            {
                FullName = dto.FullName,
                Email = dto.Email,
                ServiceOption = dto.ServiceOption,
                Message = dto.Message
            };
        }
        catch { }
        return null!;
    }
}
