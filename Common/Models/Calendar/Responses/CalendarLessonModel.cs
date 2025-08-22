namespace Common.Models.Calendar.Responses
{
    public class CalendarLessonModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public DateTime LessonDate { get; set; }
    }
}
