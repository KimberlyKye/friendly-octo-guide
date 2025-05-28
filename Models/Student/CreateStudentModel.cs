using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Student
{
    /// <summary>
    /// Модель для создания профиля студента
    /// </summary>
    public class CreateStudentModel
    {
        /// <summary>
        /// Пароль для создания профиля
        /// </summary>
        public required string Password { get; set; }

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
