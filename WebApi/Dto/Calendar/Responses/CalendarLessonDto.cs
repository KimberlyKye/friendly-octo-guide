namespace WebApi.Dto.Calendar.Responses
{
    public class CalendarLessonDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public string LessonName { get; set; }
    }
}
