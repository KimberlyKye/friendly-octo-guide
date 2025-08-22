namespace WebApi.Dto.Calendar.Responses
{
    /// <summary>
    /// DTO для возврата данных о занятиях для календаря 
    /// </summary>
    public class CalendarLessonDto
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// Идентификатор занятия
        /// </summary>
        public int LessonId { get; set; }

        /// <summary>
        /// Название занятия
        /// </summary>
        public string LessonName { get; set; }

        /// <summary>
        /// Дата проведения занятия
        /// </summary>
        public DateTime LessonDate { get; set; }
    }
}
