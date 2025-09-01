namespace Infrastructure.DataModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Material { get; set; }
        public int Score { get; set; }
    }
}
