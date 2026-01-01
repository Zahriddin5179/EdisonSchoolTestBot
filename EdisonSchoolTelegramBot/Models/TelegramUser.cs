using System.ComponentModel.DataAnnotations;

namespace EdisonSchoolTelegramBot.Models
{
    public class TelegramUser
    {
        [Key]
        public long ChatId { get; set; }

        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
