using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects.Base;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Класс для работы с номерами мобильных телефонов российских операторов.
    /// Обеспечивает валидацию и нормализацию номеров.
    /// </summary>
    public class PhoneNumber : ValueObject<string>
    {
        /// <summary>
        /// Нормализованный номер телефона в формате +7XXXXXXXXXX
        /// </summary>
        private readonly string _phoneNumber;

        /// <summary>
        /// Возвращает нормализованное значение номера телефона
        /// </summary>
        public string Value => _phoneNumber;

        /// <summary>
        /// Конструктор класса PhoneNumber.
        /// Выполняет валидацию и нормализацию номера телефона.
        /// </summary>
        /// <param name="phoneNumber">Номер телефона в произвольном формате</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если передан пустой номер</exception>
        /// <exception cref="ArgumentException">Выбрасывается, если номер не соответствует формату российских мобильных номеров</exception>
        public PhoneNumber(string phoneNumber) : base(phoneNumber)
        {
            // Проверка на null или пустую строку
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber),
                    "Номер телефона не может быть пустым или состоять только из пробелов");
            }

            // Нормализация номера (приведение к единому формату)
            _phoneNumber = NormalizePhoneNumber(phoneNumber);

            // Валидация номера (соответствие российскому мобильному формату)
            if (!IsValidRussianMobilePhone(_phoneNumber))
            {
                throw new ArgumentException(
                    "Неверный формат номера телефона. Ожидается российский мобильный номер (+79XXXXXXXXX)",
                    nameof(phoneNumber));
            }
        }

        /// <summary>
        /// Нормализует номер телефона, приводя его к единому формату +7XXXXXXXXXX
        /// </summary>
        /// <param name="phoneNumber">Исходный номер телефона</param>
        /// <returns>Нормализованный номер телефона</returns>
        private static string NormalizePhoneNumber(string phoneNumber)
        {
            // Удаление всех нецифровых символов, кроме '+' в начале
            var normalized = new string(phoneNumber
                .Where(c => char.IsDigit(c) || c == '+')
                .ToArray());

            // Преобразование номера, начинающегося с 8, в формат +7
            if (normalized.StartsWith("8"))
            {
                normalized = "+7" + normalized[1..];
            }
            // Добавление '+' к номерам, начинающимся с 7 (если '+' отсутствует)
            else if (normalized.StartsWith("7") && normalized.Length == 11)
            {
                normalized = "+" + normalized;
            }
            // Добавление '+' для всех остальных случаев (если '+' отсутствует)
            else if (!normalized.StartsWith("+"))
            {
                normalized = "+" + normalized;
            }

            return normalized;
        }

        /// <summary>
        /// Проверяет, соответствует ли номер формату российского мобильного телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона в формате +7XXXXXXXXXX</param>
        /// <returns>true - если номер валиден, false - в противном случае</returns>
        private static bool IsValidRussianMobilePhone(string phoneNumber)
        {
            // Проверка длины номера и начала с +7
            if (phoneNumber.Length != 12 || !phoneNumber.StartsWith("+7"))
                return false;

            // Извлечение кода оператора (3 цифры после +7)
            var operatorCode = phoneNumber.Substring(2, 3);

            // Проверка, что код оператора состоит только из цифр
            if (!int.TryParse(operatorCode, out var code))
                return false;

            // Проверка, что код оператора находится в диапазоне 900-999
            return code is >= 900 and <= 999;
        }

        /// <summary>
        /// Возвращает строковое представление номера телефона
        /// </summary>
        public override string ToString() => _phoneNumber;

        /// <summary>
        /// Неявное преобразование PhoneNumber в string
        /// </summary>
        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber._phoneNumber;
    }
}
