using Common.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace Common.Domain.ValueObjects
{
    /// <summary>
    /// Название домашнего задания с валидацией и возможностью изменения
    /// </summary>
    /// <remarks>
    /// Обеспечивает:
    /// 1. Валидацию при создании и изменении
    /// 2. Автоматическую нормализацию (удаление лишних пробелов)
    /// 3. Безопасное изменение значения через свойство Value
    /// </remarks>
    public class HomeTaskName : ValueObject<string>
    {
        private const int MinLength = 2;
        private const int MaxLength = 100;
        private static readonly Regex ValidCharsRegex = new(@"^[\p{L}\d\s\-_№]+$", RegexOptions.Compiled);
        private string _name;

        /// <summary>
        /// Инициализирует новый экземпляр названия домашнего задания
        /// </summary>
        /// <param name="name">Название задания</param>
        /// <exception cref="ArgumentNullException">Если название null или пустое</exception>
        /// <exception cref="ArgumentException">Если название не соответствует требованиям</exception>
        public HomeTaskName(string name) : base(ValidateAndNormalize(name))
        {
            _name = base.Value;
        }

        /// <summary>
        /// Текущее значение названия
        /// </summary>
        /// <remarks>
        /// При установке значения выполняет валидацию и нормализацию
        /// </remarks>
        public new string Value
        {
            get => _name;
            set => _name = ValidateAndNormalize(value);
        }

        /// <summary>
        /// Проверяет и нормализует название задания
        /// </summary>
        /// <param name="name">Входное название</param>
        /// <returns>Нормализованное название</returns>
        private static string ValidateAndNormalize(string name)
        {
            // Проверка на null или пустую строку
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Название задания не может быть пустым");

            // Удаление лишних пробелов
            var trimmedName = name.Trim();

            // Проверка длины
            if (trimmedName.Length < MinLength)
                throw new ArgumentException(
                    $"Название задания должно содержать минимум {MinLength} символа",
                    nameof(name));

            if (trimmedName.Length > MaxLength)
                throw new ArgumentException(
                    $"Название задания должно содержать максимум {MaxLength} символов",
                    nameof(name));

            // Проверка допустимых символов
            if (!ValidCharsRegex.IsMatch(trimmedName))
                throw new ArgumentException(
                    "Название задания может содержать только буквы, цифры, пробелы и дефисы",
                    nameof(name));

            return trimmedName;
        }

        /// <summary>
        /// Неявное преобразование в строку
        /// </summary>
        public static implicit operator string(HomeTaskName name) => name.Value;

        /// <summary>
        /// Возвращает строковое представление
        /// </summary>
        public override string ToString() => Value;
    }
}