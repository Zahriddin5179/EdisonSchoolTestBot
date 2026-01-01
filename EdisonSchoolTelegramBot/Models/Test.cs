using System.ComponentModel.DataAnnotations;

namespace EdisonSchoolTelegramBot.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public bool IsActive { get; set; }
    }


}
