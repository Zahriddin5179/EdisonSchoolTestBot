using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdisonSchoolTelegramBot.Models
{
    public class TestOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public TestQuestion Question { get; set; } = null!;

        [Required]
        public string OptionText { get; set; } = null!;

        public bool IsCorrect { get; set; }

        [MaxLength(1)]
        public string Label { get; set; } = null!; // A, B, C, D
    }
}
