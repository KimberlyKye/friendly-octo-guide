namespace WebApi.Dto.Teacher.Responses
{
    public class CalendarResponseDto
    {
        public CalendarLessonDto[] CalendarLessonDtos {  get; set; } = new CalendarLessonDto[0];

        public CalendarHomeTaskDto[] CalendarHomeTaskDtos { get; set; } = new CalendarHomeTaskDto[0];
    }    
}
