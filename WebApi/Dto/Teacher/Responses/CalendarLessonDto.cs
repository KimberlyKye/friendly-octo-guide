namespace WebApi.Dto.Teacher.Responses
{
    public class CalendarLessonDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
