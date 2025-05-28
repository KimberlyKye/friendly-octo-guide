namespace WebApi.Dto.Calendar.Responses
{
    /// <summary>
    /// DTO для возврата данных о домашних заданиях для календаря 
    /// </summary>
    public class CalendarHomeTaskDto
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
        /// Идентификатор домашнего задания
        /// </summary>
        public int HomeTaskId { get; set; }

        /// <summary>
        /// Название домашнего задания
        /// </summary>
        public string HomeTaskName { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Дата дедлайна сдачи дз
        /// </summary>
        public DateTime DateEnd { get; set; }               
    }
}
