using System.ComponentModel.DataAnnotations;

namespace EdisonSchoolTelegramBot.Models
{
    public class TestAttemptAnswer
    {
        public int Id { get; set; }

        public int AttemptId { get; set; }
        public int QuestionId { get; set; }

        public int? OptionId { get; set; }      // yopiq savol uchun
        public string? TextAnswer { get; set; } // ochiq savol uchun

        public bool? IsCorrect { get; set; } // yopiq: avtomatik, ochiq: keyin tekshiriladi
    }

}
