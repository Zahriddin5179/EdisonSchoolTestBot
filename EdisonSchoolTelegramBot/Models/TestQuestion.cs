using EdisonSchoolTelegramBot.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdisonSchoolTelegramBot.Models
{
    public class TestQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; } = null!;

        [Required]
        public string QuestionText { get; set; } = null!;

        public int Order { get; set; }

        public QuestionType QuestionType { get; set; }

        public string? CorrectTextAnswer { get; set; }

    }
}
