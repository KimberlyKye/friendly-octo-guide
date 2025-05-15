using Entities;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using File = Domain.ValueObjects.File;

namespace WebApi.Dto.Teacher.Requests
{

    /// <summary>
    /// Модель для создания занятия
    /// </summary>
    public class CreateLessonRequestDto
    {
        /// <summary>
        /// Id преподавателя, создающего занятие
        /// </summary>
        [Required(ErrorMessage = "Id преподавателя обязателен")]
        [Range(1, long.MaxValue, ErrorMessage = "Id преподавателя должен быть положительным числом")]
        public long TeacherId { get; set; }

        /// <summary>
        /// Id курса, которому будет принадлежать занятие
        /// </summary>
        [Required(ErrorMessage = "Id курса обязателен")]
        [Range(1, long.MaxValue, ErrorMessage = "Id курса должен быть положительным числом")]
        public long CourseId { get; set; }

        /// <summary>
        /// Название занятия
        /// </summary>
        [Required(ErrorMessage = "Название занятия обязателено")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина названия должна быть от 2 до 100 символов.")]
        [RegularExpression(@"^[\p{L}\d\s\-,.:;!?()_№]+$",
        ErrorMessage = "Название может содержать только буквы, цифры, пробелы и ,.-:;!?()_№")]
        public string LessonName { get; set; }

        /// <summary>
        /// Описание занятия
        /// </summary>
        [Required(ErrorMessage = "Описание занятия обязателено")]
        public string LessonDescription { get; set; }

        /// <summary>
        /// Дата проведения занятия с точностью до минуты
        /// </summary>
        [Required(ErrorMessage = "Дата проведения занятия обязателена")]
        public DateTime LessonStartDate { get; set; }

        /// <summary>
        /// Учебные материалы, прилагаемые к занятию
        /// </summary>
        public File? Material { get; set; }
    }
}
