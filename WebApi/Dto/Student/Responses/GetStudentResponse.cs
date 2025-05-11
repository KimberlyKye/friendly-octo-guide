namespace WebApi.Dto.Student
{
    /// <summary>
    /// Модель студента
    /// </summary>
    public class GetStudentResponse
    {
        /// <summary>
        /// ID студента
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
        /// Имя студента
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Фамилия студента
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Дата рождения студента
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
