using Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueObjects
{
    /// <summary>
    /// День рождения преподавателя.
    /// </summary>
    public class DateBirthTeacher : IValueObject
    {
        /// <summary>
        /// День рождения.
        /// </summary>
        private DateOnly _date;

        /// <summary>
        ///  Конструктор.
        /// </summary>
        /// <param name="date">День рождения.</param>
        /// <exception cref="ArgumentException"></exception>
        public DateBirthTeacher(DateOnly date)
        {
            // Получаем текущую дату
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Проверка на минимальный возраст (8 лет)
            if (date > today.AddYears(-18))
            {
                throw new ArgumentException("Возраст преподавателя не может быть меньше 18 лет", nameof(date));
            }

            // Проверка на максимальный возраст (100 лет)
            if (date < today.AddYears(-100))
            {
                throw new ArgumentException("Возраст студента не может быть больше 100 лет", nameof(date));
            }
            _date = date;
        }
        // <summary>
        /// Геттеры.
        /// </summary>
        public DateOnly Date => _date;
    }
}
