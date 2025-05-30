using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Teacher.Requests
{
    /// <summary>
    /// Метод на обновление информации в профиле преподавателя
    /// </summary>
    public class UpdateTeacherRequest
    {
        /// <summary>
        /// ID преподавателя
        /// </summary>
        [Required(ErrorMessage = "ID преподавателя обязателен")]
        public int Id { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")] // Проверяет валидный email
        public string Email { get; set; }

        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [Phone(ErrorMessage = "Неправильный номер телефона")] // Стандартная проверка телефонного номера
                                                              //[RegularExpression(@"\+7\d{10}", ErrorMessage = "Телефон должен быть указан в формате +7XXXXXXXXXX")] // Регулярное выражение для российского номера телефона
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Имя преподавателя
        /// </summary>
        [Required(ErrorMessage = "Имя обязательно")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия преподавателя
        /// </summary>
        [Required(ErrorMessage = "Фамилия обязательна")]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения преподавателя
        /// </summary>
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)] // Указываем тип поля как дата
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
    }
}
