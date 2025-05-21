namespace WebApi.Dto.Calendar.Responses
{
    /// <summary>
    /// DTO для возврата данных для календаря преподавателя
    /// </summary>
    public class TeacherCalendarResponseDto
    {
        /// <summary>
        /// Массив данных о занятиях
        /// </summary>
        public CalendarLessonDto[] CalendarLessonDtos { get; set; } = new CalendarLessonDto[0];
    }
}
