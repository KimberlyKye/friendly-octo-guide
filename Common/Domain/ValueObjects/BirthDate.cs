using Common.Domain.ValueObjects.Base;

namespace Common.Domain.ValueObjects
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

            // Проверка на минимальный возраст (18 лет)
            if (date > today.AddYears(-18))
            {
                throw new ArgumentException("Возраст не может быть меньше 18 лет", nameof(date));
            }

            // Проверка на максимальный возраст (130 лет)
            if (date < today.AddYears(-130))
            {
                throw new ArgumentException("Возраст не может быть больше 130 лет", nameof(date));
            }
            _date = date;
        }

        /// <summary>
        /// Переопределение конструктора для создания BirthDate из типа DateTime
        /// </summary>
        /// <param name="birthDate"></param>
        /// <exception cref="ArgumentException"></exception>
        public BirthDate(DateTime birthDate) : base(DateOnly.FromDateTime(birthDate))
        {
            // Получаем текущую дату
            var today = DateOnly.FromDateTime(DateTime.Now);
            var date = DateOnly.FromDateTime(birthDate);

            // Проверка на минимальный возраст (18 лет)
            if (date > today.AddYears(-18))
            {
                throw new ArgumentException("Возраст не может быть меньше 18 лет", nameof(birthDate));
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
