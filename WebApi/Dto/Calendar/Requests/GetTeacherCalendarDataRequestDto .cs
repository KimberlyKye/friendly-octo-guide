using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Calendar.Requests
{
    public class GetTeacherCalendarDataRequestDto
    {
        /// <summary>
        /// Id преподавателя, создающего занятие
        /// </summary>
        [Required(ErrorMessage = "Id преподавателя обязателен")]
        [Range(1, long.MaxValue, ErrorMessage = "Id преподавателя должен быть положительным числом")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Дата обязательна")]
        public DateOnly Date { get; set; }
    }
}
