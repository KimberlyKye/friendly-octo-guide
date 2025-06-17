using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Teacher.Requests
{
    /// <summary>
    /// Запрос на получение информации о преподавателе по ID
    /// </summary>
    public class GetTeacherRequest
    {
        /// <summary>
        /// ID преподавателя
        /// </summary>
        [Required(ErrorMessage = "ID преподавателя обязателен")]
        public int Id { get; set; }
    }
}
