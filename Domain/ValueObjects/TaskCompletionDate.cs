using Domain.ValueObjects.Base;

namespace ValueObjects
{
    /// <summary>
    /// Представляет дату завершения задачи как объект-значение (Value Object)
    /// </summary>
    /// <remarks>
    /// Гарантирует валидность даты выполнения задачи, обеспечивая что:
    /// - Дата не может быть неинициализированной (по умолчанию 01.01.0001)
    /// - Не может быть в прошлом (опционально, в зависимости от бизнес-правил)
    /// - Поддерживает изменение значения через метод Update с валидацией
    /// </remarks>
    public class TaskCompletionDate : ValueObject<DateOnly>
    {
        private DateOnly _date; // Убрано readonly для поддержки изменения

        /// <summary>
        /// Инициализирует новый экземпляр даты завершения задачи
        /// </summary>
        /// <param name="date">Дата завершения</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается если дата:
        /// - Равна default (01.01.0001)
        /// - Раньше текущей даты (если это запрещено бизнес-правилами)
        /// </exception>
        public TaskCompletionDate(DateOnly date) : base(date)
        {
            ValidateDate(date);
            _date = date;
        }

        /// <summary>
        /// Инициализирует новый экземпляр даты завершения задачи
        /// </summary>
        /// <param name="date">Дата завершения</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается если дата:
        /// - Равна default (01.01.0001)
        /// - Раньше текущей даты (если это запрещено бизнес-правилами)
        /// </exception>
        public TaskCompletionDate(DateTime date) : base(DateOnly.FromDateTime(date))
        {
            var convertetDate = DateOnly.FromDateTime(date);
            ValidateDate(convertetDate);
            _date = convertetDate;
        }


        /// <summary>
        /// Текущее значение даты завершения задачи
        /// </summary>
        public DateOnly Value
        {
            get => _date;
            protected set
            {
                ValidateDate(value);
                _date = value;
            }
        }

        /// <summary>
        /// Обновляет дату завершения задачи
        /// </summary>
        /// <param name="newDate">Новая дата завершения</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается при тех же условиях, что и в конструкторе
        /// </exception>
        public void Update(DateOnly newDate)
        {
            Value = newDate;
        }

        /// <summary>
        /// Проверяет валидность даты
        /// </summary>
        /// <param name="date">Проверяемая дата</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается при невалидной дате
        /// </exception>
        private static void ValidateDate(DateOnly date)
        {
            if (date == default)
                throw new ArgumentException("Дата завершения не может быть неинициализированной");

            if (date < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Дата завершения не может быть в прошлом");
        }

        /// <summary>
        /// Проверяет, является ли дата завершения просроченной
        /// </summary>
        /// <returns>
        /// true - если текущая дата превышает дату завершения,
        /// false - если срок еще не наступил
        /// </returns>
        public bool IsOverdue() => _date < DateOnly.FromDateTime(DateTime.Now);

        /// <summary>
        /// Возвращает количество оставшихся дней до завершения задачи
        /// </summary>
        /// <returns>
        /// Положительное число - дней осталось,
        /// Отрицательное - если срок прошел,
        /// 0 - если срок сегодня
        /// </returns>
        public int DaysRemaining() => _date.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;
    }
}