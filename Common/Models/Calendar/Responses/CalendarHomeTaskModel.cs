namespace Common.Models.Calendar.Responses
{
    public class CalendarHomeTaskModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public int HomeTaskId { get; set; }
        public string HomeTaskName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
