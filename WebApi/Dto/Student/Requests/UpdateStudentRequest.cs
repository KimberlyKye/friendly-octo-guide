using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Student.Requests
{
    /// <summary>
    /// Метод на обновление информации в профиле студента
    /// </summary>
    public class UpdateStudentRequest
    {
        /// <summary>
        /// ID студента
        /// </summary>
        [Required(ErrorMessage = "ID студента обязателен")]
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
        /// Имя студента
        /// </summary>
        [Required(ErrorMessage = "Имя обязательно")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия студента
        /// </summary>
        [Required(ErrorMessage = "Фамилия обязательна")]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения студента
        /// </summary>
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)] // Указываем тип поля как дата
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
    }
}
