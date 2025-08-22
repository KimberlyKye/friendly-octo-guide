using Common.Domain.ValueObjects.Base;

namespace Common.Domain.ValueObjects
{
    /// <summary>
    /// Название курса.
    /// </summary>
    public class CourseName : ValueObject<string>
    {
        /// <summary>
        /// Название курса.
        /// </summary>
        private string _name;

        /// <summary>
        ///  Конструктор.
        /// </summary>
        /// <param name="firstName">Название курса.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CourseName(string name) : base(name)
        {
            //Проверка на пустое значение.           
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Название курса не введено", nameof(name));
            }

            //Проверка на длину названия.            
            if (name.Length < 2 || name.Length > 50)
            {
                throw new ArgumentException("Название должно быть длиной от 2 до 50 символов", nameof(name));
            }
            _name = name;
        }

        /// <summary>
        /// Геттеры.
        /// </summary>
        public string Name => _name;
        public static implicit operator string(CourseName courseName) => courseName._name;

    }
}
