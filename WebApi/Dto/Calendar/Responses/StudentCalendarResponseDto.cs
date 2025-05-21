namespace WebApi.Dto.Calendar.Responses
{
    /// <summary>
    /// DTO для возврата данных для календаря студента
    /// </summary>
    public class StudentCalendarResponseDto
    {
        /// <summary>
        /// Массив данных о занятиях
        /// </summary>
        public CalendarLessonDto[] CalendarLessonDtos { get; set; } = new CalendarLessonDto[0];

        /// <summary>
        /// Массив данных о домашних заданиях
        /// </summary>
        public CalendarHomeTaskDto[] CalendarHomeTaskDtos { get; set; } = new CalendarHomeTaskDto[0];
    }
}
