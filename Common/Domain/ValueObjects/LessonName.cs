using Common.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace Common.Domain.ValueObjects
{
    /// <summary>
    /// Название урока с валидацией и возможностью изменения
    /// </summary>
    /// <remarks>
    /// Обеспечивает:
    /// 1. Более гибкие правила валидации (допускает знаки препинания)
    /// 2. Поддержку изменения значения с сохранением валидации
    /// 3. Автоматическую нормализацию ввода
    /// </remarks>
    public class LessonName : ValueObject<string>
    {
        public const int MinLength = 2;
        public const int MaxLength = 100;
        private static readonly Regex ValidCharsRegex = new(@"^[\p{L}\d\s\-,.:;!?()_№]+$", RegexOptions.Compiled);
        private string _name;

        /// <summary>
        /// Инициализирует новый экземпляр названия урока
        /// </summary>
        /// <param name="name">Название урока</param>
        /// <exception cref="ArgumentNullException">Если название null или пустое</exception>
        /// <exception cref="ArgumentException">Если название не соответствует требованиям</exception>
        public LessonName(string name) : base(ValidateAndNormalize(name))
        {
            _name = base.Value;
        }

        /// <summary>
        /// Текущее значение названия
        /// </summary>
        /// <remarks>
        /// При установке автоматически выполняет валидацию и нормализацию
        /// </remarks>
        public new string Value
        {
            get => _name;
            set => _name = ValidateAndNormalize(value);
        }

        /// <summary>
        /// Проверяет и нормализует название урока
        /// </summary>
        private static string ValidateAndNormalize(string name)
        {
            // Проверка на null или пустую строку
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Название урока не может быть пустым");

            // Нормализация пробелов
            var trimmedName = name.Trim();

            // Проверка длины
            if (trimmedName.Length < MinLength)
                throw new ArgumentException(
                    $"Название урока должно содержать минимум {MinLength} символа",
                    nameof(name));

            if (trimmedName.Length > MaxLength)
                throw new ArgumentException(
                    $"Название урока должно содержать максимум {MaxLength} символов",
                    nameof(name));

            // Проверка допустимых символов
            if (!ValidCharsRegex.IsMatch(trimmedName))
                throw new ArgumentException(
                    "Название урока может содержать только буквы, цифры, пробелы и ,.-:;!?()",
                    nameof(name));

            return trimmedName;
        }

        /// <summary>
        /// Неявное преобразование в строку
        /// </summary>
        public static implicit operator string(LessonName name) => name.Value;

        /// <summary>
        /// Явное преобразование из строки
        /// </summary>
        public static explicit operator LessonName(string name) => new(name);

        /// <summary>
        /// Возвращает строковое представление
        /// </summary>
        public override string ToString() => Value;
    }
}