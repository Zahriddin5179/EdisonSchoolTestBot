using System.ComponentModel.DataAnnotations;

namespace EdisonSchoolTelegramBot.Models
{
    public class TestAttempt
    {
        [Key]
        public int Id { get; set; }

        public long ChatId { get; set; }
        public int TestId { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FinishedAt { get; set; }
    }
}
