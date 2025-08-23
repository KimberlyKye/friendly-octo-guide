namespace WebApi.Dto.Student.Requests
{
    /// <summary>
    /// Запрос на добавление студентов в курс
    /// </summary>
    public class StudentsToCourseRequest
    {
        /// <summary>
        /// ID курса, в который добавляются студенты
        /// </summary>
        /// <value></value>
        public int courseId { get; set; }
        /// <summary>
        /// ID студентов, которые добавляются в курс
        /// </summary>
        /// <value></value>
        public int[] studentIds { get; set; }
    }
}