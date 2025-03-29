using Domain.ValueObjects.Base;
using System;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Дата рождения студента.
    /// Гарантирует валидность даты (возраст от 8 до 100 лет).
    /// </summary>
    public readonly struct StudentBirthDate : ValueObject<DateOnly>, IEquatable<StudentBirthDate>
    {
        private readonly DateOnly _date;
        private const int MinStudentAge = 8;
        private const int MaxStudentAge = 100;

        /// <summary>
        /// Дата рождения студента
        /// </summary>
        public DateOnly Date => _date;

        /// <summary>
        /// Текущий возраст студента (в полных годах)
        /// </summary>
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var age = today.Year - _date.Year;
                // Корректировка, если день рождения в этом году еще не наступил
                if (_date > today.AddYears(-age)) age--;
                return age;
            }
        }

        /// <summary>
        /// Создает новую дату рождения студента
        /// </summary>
        /// <param name="date">Дата рождения</param>
        /// <exception cref="ArgumentException">
        /// Возникает, если возраст меньше 8 или больше 100 лет
        /// </exception>
        public StudentBirthDate(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (date > today.AddYears(-MinStudentAge))
                throw new ArgumentException(
                    $"Студент должен быть старше {MinStudentAge} лет",
                    nameof(date));

            if (date < today.AddYears(-MaxStudentAge))
                throw new ArgumentException(
                    $"Студент должен быть моложе {MaxStudentAge} лет",
                    nameof(date));

            _date = date;
        }

        /// <summary>
        /// Проверяет, является ли дата допустимой датой рождения студента
        /// </summary>
        public static bool IsValid(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return date <= today.AddYears(-MinStudentAge) &&
                   date >= today.AddYears(-MaxStudentAge);
        }

        /// <summary>
        /// Возвращает строковое представление даты в формате "dd.MM.yyyy"
        /// </summary>
        public override string ToString() => _date.ToString("dd.MM.yyyy");

        /// <summary>
        /// Преобразует в DateTime (для совместимости)
        /// </summary>
        public DateTime ToDateTime() => _date.ToDateTime(TimeOnly.MinValue);

        // Реализация IEquatable<T>
        public bool Equals(StudentBirthDate other) => _date == other._date;
        public override bool Equals(object? obj) => obj is StudentBirthDate other && Equals(other);
        public override int GetHashCode() => _date.GetHashCode();

        public static bool operator ==(StudentBirthDate left, StudentBirthDate right) => left.Equals(right);
        public static bool operator !=(StudentBirthDate left, StudentBirthDate right) => !(left == right);

        /// <summary>
        /// Неявное преобразование в DateOnly
        /// </summary>
        public static implicit operator DateOnly(StudentBirthDate date) => date._date;

        /// <summary>
        /// Явное преобразование из DateOnly с проверкой возраста
        /// </summary>
        public static explicit operator StudentBirthDate(DateOnly date) => new(date);
        
    }
}