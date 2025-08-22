namespace Infrastructure.DataModels
{
    public class LessonScore
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int Score { get; set; }
    }
}