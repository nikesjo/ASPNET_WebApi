using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Email { get; set; } = null!;

    public bool DailyNewsLetter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekInReview { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }

    public static implicit operator SubscriberEntity(SubscriberDto dto)
    {
        return new SubscriberEntity
        {
            Email = dto.Email,
            DailyNewsLetter = dto.DailyNewsLetter,
            AdvertisingUpdates = dto.AdvertisingUpdates,
            WeekInReview = dto.WeekInReview,
            EventUpdates = dto.EventUpdates,
            StartupsWeekly = dto.StartupsWeekly,
            Podcasts = dto.Podcasts
        };
    }
}
