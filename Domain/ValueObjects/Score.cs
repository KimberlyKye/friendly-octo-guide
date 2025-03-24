using Domain.ValueObjects.Base;
using System;

namespace ValueObjects
{
    /// <summary>
    /// Значение баллов за домашнее задание.
    /// Гарантирует валидность значения баллов (0-100) и предоставляет методы для работы с ними.
    /// </summary>
    public readonly struct Score : IValueObject,IEquatable<Score>
    {
        private readonly int _value;

        /// <summary>
        /// Текущее значение баллов
        /// </summary>
        public int Value => _value;

        /// <summary>
        /// Максимально возможное количество баллов
        /// </summary>
        public const int MaxValue = 100;

        /// <summary>
        /// Минимально возможное количество баллов
        /// </summary>
        public const int MinValue = 0;

        /// <summary>
        /// Создает новый экземпляр структуры Score с указанным количеством баллов
        /// </summary>
        /// <param name="score">Количество баллов (должно быть в диапазоне 0-100)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если значение не входит в допустимый диапазон
        /// </exception>
        public Score(int score)
        {
            if (score < MinValue || score > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(score),
                    $"Количество баллов должно быть в диапазоне от {MinValue} до {MaxValue}. Получено: {score}");
            }

            _value = score;
        }

        /// <summary>
        /// Проверяет, является ли значение допустимым количеством баллов
        /// </summary>
        public static bool IsValid(int score) => score >= MinValue && score <= MaxValue;

        /// <summary>
        /// Неявное преобразование Score в int
        /// </summary>
        public static implicit operator int(Score score) => score._value;

        /// <summary>
        /// Явное преобразование int в Score
        /// </summary>
        public static explicit operator Score(int score) => new(score);

        /// <summary>
        /// Возвращает строковое представление количества баллов
        /// </summary>
        public override string ToString() => _value.ToString();

        /// <summary>
        /// Определяет равенство значений баллов
        /// </summary>
        public bool Equals(Score other) => _value == other._value;

        /// <summary>
        /// Определяет равенство значений баллов
        /// </summary>
        public override bool Equals(object? obj) => obj is Score other && Equals(other);

        /// <summary>
        /// Возвращает хэш-код объекта
        /// </summary>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Оператор равенства
        /// </summary>
        public static bool operator ==(Score left, Score right) => left.Equals(right);

        /// <summary>
        /// Оператор неравенства
        /// </summary>
        public static bool operator !=(Score left, Score right) => !(left == right);
    }
}