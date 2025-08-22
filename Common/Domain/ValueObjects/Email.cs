using Common.Domain.ValueObjects.Base;
using System.Net.Mail;

namespace Common.Domain.ValueObjects
{
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public class Email : ValueObject<string>
    {
        /// <summary>
        /// Электронная почта.
        /// </summary>
        private string _email; //Нужно ли использовать readonly ?

        /// <summary>
        ///  Конструктор.
        /// </summary>
        /// <param name="email">Электронная почта.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Email(string email) : base(email)
        {
            //Проверка на пустое значение.
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("Email не может быть пустым", nameof(email));
            }
            //Валидация.
            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Некорректный формат email", nameof(email));
            }
            _email = email;
        }
        /// <summary>
        /// Геттеры.
        /// </summary>
        public string Value => _email;
        private static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
