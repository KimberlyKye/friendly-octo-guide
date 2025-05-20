namespace WebApi.Dto.Calendar.Responses
{
    public class StudentCalendarResponseDto
    {
        public CalendarLessonDto[] CalendarLessonDtos { get; set; } = new CalendarLessonDto[0];

        public CalendarHomeTaskDto[] CalendarHomeTaskDtos { get; set; } = new CalendarHomeTaskDto[0];
    }
}
