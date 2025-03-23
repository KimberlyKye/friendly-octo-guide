using Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ValueObjects
{
    /// <summary>
    /// День рождения студента.
    /// </summary>
    public class DateBirthStudent : IValueObject
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
        public DateBirthStudent(DateOnly date)
        {
            // Получаем текущую дату
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Проверка на минимальный возраст (8 лет)
            if (date > today.AddYears(-8))
            {
                throw new ArgumentException("Возраст студента не может быть меньше 8 лет", nameof(date));
            }

            // Проверка на максимальный возраст (100 лет)
            if (date < today.AddYears(-100))
            {
                throw new ArgumentException("Возраст студента не может быть больше 100 лет", nameof(date));
            }
            _date = date;
        }
        public DateOnly Date => _date;
    }
}
