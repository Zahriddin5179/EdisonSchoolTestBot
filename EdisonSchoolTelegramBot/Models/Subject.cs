namespace EdisonSchoolTelegramBot.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }

}
