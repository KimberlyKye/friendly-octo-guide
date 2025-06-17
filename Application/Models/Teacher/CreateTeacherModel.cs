using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher
{
    /// <summary>
    /// Модель для создания профиля преподавателя
    /// </summary>
    public class CreateTeacherModel
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
