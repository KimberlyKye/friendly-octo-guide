using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Calendar.Requests
{
    /// <summary>
    /// DTO для запроса данных календаря преподавателя
    /// </summary>
    public class GetTeacherCalendarDataRequestDto
    {
        /// <summary>
        /// Идентификатор преподавателя
        /// </summary>
        /// <example>12345</example>
        [Required(ErrorMessage = "Id преподавателя обязателен")]
        [Range(1, long.MaxValue, ErrorMessage = "Id преподавателя должен быть положительным числом")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Начальная дата периода для получения данных календаря (формат: YYYY-MM-DD)
        /// </summary>
        /// <example>2023-12-31</example>
        [Required(ErrorMessage = "Начальная дата обязательна")]
        [DataType(DataType.Date, ErrorMessage = "Некорректный формат даты")]
        public DateOnly StartDate { get; set; }

        /// <summary>
        /// Конечная дата периода для получения данных календаря (формат: YYYY-MM-DD)
        /// </summary>
        /// <example>2023-12-31</example>
        [Required(ErrorMessage = "Конечная дата обязательна")]
        [DataType(DataType.Date, ErrorMessage = "Некорректный формат даты")]
        public DateOnly EndDate { get; set; }
    }
}
