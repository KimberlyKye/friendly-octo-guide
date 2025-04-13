
namespace Dto
{
    public class CalendarResponseDto
    {
        public CalendarLessonDto[] calendarLessonDtos {  get; set; } = new CalendarLessonDto[0];

        public CalendarHomeTaskDto[] calendarHomeTaskDtos { get; set; } = new CalendarHomeTaskDto[0];
    }
    public class CalendarLessonDto
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
    }
    public class CalendarHomeTaskDto
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public int lessonId { get; set; }
        public string lessonName { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public string name { get; set; }
    }
}
