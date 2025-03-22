using System.Text.RegularExpressions;

namespace Domain.ValueObjects;
/// <summary>
/// Полное имя пользователя.
/// </summary>
public class FullName : IValueObject
{
    /// <summary>
    /// Имя.
    /// </summary>
    private string _firstName;

    /// <summary>
    /// Фамилия.
    /// </summary>
    private string _lastName;

    /// <summary>
    ///  Конструктор.
    /// </summary>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public FullName(string firstName, string lastName)
    {
        /*
        Проверка на пустые значения.
        */
        if (string.IsNullOrEmpty(firstName))
        {
            throw new ArgumentNullException("Имя не введено", nameof(firstName));
        }

        if (string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentNullException("Фамилия не введена", nameof(lastName));
        }

        /*
        Проверка на длину имени и фамилии.
        */

        if (firstName.Length < 2 || firstName.Length > 50)
        {
            throw new ArgumentException("Имя должно быть длиной от 2 до 50 символов", nameof(firstName));
        }

        if (lastName.Length < 2 || lastName.Length > 50)
        {
            throw new ArgumentException("Фамилия должна быть длиной от 2 до 50 символов", nameof(lastName));
        }


        /*
        Проверка на наличие только буквенных символов и пробелов (для случаев, когда имя или фамилия могут состоять из нескольких слов).
        */

        if (!Regex.IsMatch(firstName, @"^[a-zA-Zа-яА-Я\s]+$"))
        {
            throw new ArgumentException("Имя должно содержать только буквы и пробелы", nameof(firstName));
        }

        if (!Regex.IsMatch(lastName, @"^[a-zA-Zа-яА-Яs]+$"))
        {
            throw new ArgumentException("Фамилия должна содержать только буквы и пробелы", nameof(lastName));
        }


        _firstName = firstName;
        _lastName = lastName;
    }

    /// <summary>
    /// Геттеры.
    /// </summary>
    public string FirstName { get { return _firstName; } } // можно стрелочную запись
    public string LastName { get { return _lastName; } }

    /// <summary>
    /// Метод получения полного имени.
    /// </summary>
    /// <returns></returns>
    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}
