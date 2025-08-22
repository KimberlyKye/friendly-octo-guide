using Domain.ValueObjects.Base;
using System;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Неизменяемый временной период с фиксированными начальной и конечной датами.
    /// Гарантирует валидность периода (начальная дата всегда меньше конечной).
    /// </summary>
    public readonly struct Duration : IValueObject, IEquatable<Duration>
    {
        /// <summary>
        /// Начальная дата периода.
        /// </summary>
        public DateOnly StartDate { get; }

        /// <summary>
        /// Конечная дата периода.
        /// </summary>
        public DateOnly EndDate { get; }

        /// <summary>
        /// Инициализирует новый экземпляр периода.
        /// </summary>
        /// <param name="startDate">Начальная дата периода.</param>
        /// <param name="endDate">Конечная дата периода.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если начальная дата больше или равна конечной.
        /// </exception>
        public Duration(DateOnly startDate, DateOnly endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentException(
                    "Начальная дата периода должна быть строго меньше конечной даты",
                    nameof(startDate));

            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Инициализирует новый экземпляр периода.
        /// </summary>
        /// <param name="startDate">Начальная дата периода.</param>
        /// <param name="endDate">Конечная дата периода.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если начальная дата больше или равна конечной.
        /// </exception>
        public Duration(DateTime startDateTime, DateTime endDateTime)
        {
            var startDate = DateOnly.FromDateTime(startDateTime);
            var endDate = DateOnly.FromDateTime(endDateTime);

            if (startDate >= endDate)
                throw new ArgumentException(
                    "Начальная дата периода должна быть строго меньше конечной даты",
                    nameof(startDate));

            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Деконструирует период на начальную и конечную даты.
        /// </summary>
        /// <param name="startDate">Начальная дата периода.</param>
        /// <param name="endDate">Конечная дата периода.</param>
        public void Deconstruct(out DateOnly startDate, out DateOnly endDate)
        {
            startDate = StartDate;
            endDate = EndDate;
        }

        /// <summary>
        /// Проверяет, содержит ли период указанную дату.
        /// </summary>
        /// <param name="date">Проверяемая дата.</param>
        /// <returns>
        /// true - если дата находится в пределах периода (включительно), 
        /// false - в противном случае.
        /// </returns>
        public bool Contains(DateOnly date) => date >= StartDate && date <= EndDate;

        /// <summary>
        /// Проверяет пересечение с другим периодом.
        /// </summary>
        /// <param name="other">Другой период для проверки.</param>
        /// <returns>
        /// true - если периоды пересекаются, 
        /// false - в противном случае.
        /// </returns>
        public bool Overlaps(Duration other) =>
            StartDate <= other.EndDate && other.StartDate <= EndDate;

        /// <summary>
        /// Возвращает строковое представление периода в формате "StartDate - EndDate".
        /// </summary>
        /// <returns>Строковое представление периода.</returns>
        public override string ToString() => $"{StartDate:yyyy-MM-dd} — {EndDate:yyyy-MM-dd}";

        // Реализация IEquatable<Duration>
        public bool Equals(Duration other) =>
            StartDate == other.StartDate && EndDate == other.EndDate;

        public override bool Equals(object? obj) =>
            obj is Duration other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(StartDate, EndDate);

        public static bool operator ==(Duration left, Duration right) =>
            left.Equals(right);

        public static bool operator !=(Duration left, Duration right) =>
            !(left == right);
    }
}