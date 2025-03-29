using Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ValueObjects
{
    /// <summary>
    /// День рождения.
    /// </summary>
    public class BirthDate : ValueObject<DateOnly>
    {
        /// <summary>
        /// День рождения.
        /// </summary>
        private DateOnly _date;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">День рождения.</param>
        /// <exception cref="ArgumentException"></exception>
        public BirthDate(DateOnly date) : base(date)
        {
            // Получаем текущую дату
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Проверка на минимальный возраст (8 лет)
            if (date > today.AddYears(-8))
            {
                throw new ArgumentException("Возраст не может быть меньше 8 лет", nameof(date));
            }

            // Проверка на максимальный возраст (130 лет)
            if (date < today.AddYears(-130))
            {
                throw new ArgumentException("Возраст не может быть больше 130 лет", nameof(date));
            }
            _date = date;
        }
        public DateOnly Date => _date;
    }
}
