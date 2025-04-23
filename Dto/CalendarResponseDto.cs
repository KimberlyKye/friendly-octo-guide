
namespace Dto
{
    public record CalendarResponseDto
    {
        public CalendarLessonDto[] CalendarLessonDtos {  get; set; } = new CalendarLessonDto[0];

        public CalendarHomeTaskDto[] CalendarHomeTaskDtos { get; set; } = new CalendarHomeTaskDto[0];
    }
    public class CalendarLessonDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
    public class CalendarHomeTaskDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public DateTime LateStart { get; set; }
        public DateTime LateEnd { get; set; }
        public string Name { get; set; }
    }
}
