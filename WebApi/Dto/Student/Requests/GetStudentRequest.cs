using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Student.Requests
{
    /// <summary>
    /// Запрос на получение информации о студенте по ID
    /// </summary>
    public class GetStudentRequest
    {
        /// <summary>
        /// ID студента
        /// </summary>
        [Required(ErrorMessage = "ID студента обязателен")]
        public int Id { get; set; }
    }
}
