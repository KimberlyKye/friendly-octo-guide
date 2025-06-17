namespace WebApi.Dto.Teacher
{
    /// <summary>
    /// Модель обновленного преподавателя
    /// </summary>
    public class UpdateTeacherResponse
    { /// <summary>
      /// ID преподавателя
      /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Имя преподавателя
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Фамилия преподавателя
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Дата рождения преподавателя
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
