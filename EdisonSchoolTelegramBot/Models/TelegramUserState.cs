using System.ComponentModel.DataAnnotations;

namespace EdisonSchoolTelegramBot.Models
{
    public class TelegramUserState
    {
        [Key]
        public long ChatId { get; set; }

        public string State { get; set; } = "NONE";

        public int? CurrentTestId { get; set; }
        public int? CurrentQuestionId { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
