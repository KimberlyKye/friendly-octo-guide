using Domain.ValueObjects.Base;
using System;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Дата рождения преподавателя.
    /// Гарантирует валидность даты (возраст от 18 до 100 лет).
    /// </summary>
    public readonly struct TeacherBirthDate : IValueObject, IEquatable<TeacherBirthDate>
    {
        private readonly DateOnly _date;
        private const int MinTeacherAge = 18;
        private const int MaxTeacherAge = 100;

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly Date => _date;

        /// <summary>
        /// Текущий возраст преподавателя
        /// </summary>
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var age = today.Year - _date.Year;
                if (_date > today.AddYears(-age)) age--;
                return age;
            }
        }

        /// <summary>
        /// Создает новую дату рождения преподавателя
        /// </summary>
        /// <param name="date">Дата рождения</param>
        /// <exception cref="ArgumentException">
        /// Возникает если возраст меньше 18 или больше 100 лет
        /// </exception>
        public TeacherBirthDate(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (date > today.AddYears(-MinTeacherAge))
                throw new ArgumentException(
                    $"Преподаватель должен быть старше {MinTeacherAge} лет",
                    nameof(date));

            if (date < today.AddYears(-MaxTeacherAge))
                throw new ArgumentException(
                    $"Преподаватель должен быть моложе {MaxTeacherAge} лет",
                    nameof(date));

            _date = date;
        }

        /// <summary>
        /// Проверяет валидность даты рождения для преподавателя
        /// </summary>
        public static bool IsValid(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return date <= today.AddYears(-MinTeacherAge) &&
                   date >= today.AddYears(-MaxTeacherAge);
        }

        /// <summary>
        /// Возвращает строковое представление даты в формате "dd.MM.yyyy"
        /// </summary>
        public override string ToString() => _date.ToString("dd.MM.yyyy");

        /// <summary>
        /// Преобразует в DateTime (для совместимости с устаревшими системами)
        /// </summary>
        public DateTime ToDateTime() => _date.ToDateTime(TimeOnly.MinValue);

        // Реализация IEquatable<T>
        public bool Equals(TeacherBirthDate other) => _date == other._date;
        public override bool Equals(object? obj) => obj is TeacherBirthDate other && Equals(other);
        public override int GetHashCode() => _date.GetHashCode();

        public static bool operator ==(TeacherBirthDate left, TeacherBirthDate right) => left.Equals(right);
        public static bool operator !=(TeacherBirthDate left, TeacherBirthDate right) => !(left == right);

        /// <summary>
        /// Неявное преобразование в DateOnly
        /// </summary>
        public static implicit operator DateOnly(TeacherBirthDate date) => date._date;

        /// <summary>
        /// Явное преобразование из DateOnly с проверкой возраста
        /// </summary>
        public static explicit operator TeacherBirthDate(DateOnly date) => new(date);
    }
}