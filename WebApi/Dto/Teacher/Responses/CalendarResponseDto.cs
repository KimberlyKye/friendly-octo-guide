namespace WebApi.Dto.Teacher.Responses
{
    public class CalendarResponseDto
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
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Name { get; set; }
    }
}
