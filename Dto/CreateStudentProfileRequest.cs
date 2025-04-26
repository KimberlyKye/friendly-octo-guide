using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dto
{
    public record CreateStudentProfileRequest
    {
        [Required(ErrorMessage = "Имя обязательно.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Телефонный номер обязателен.")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Формат телефонного номера неверен.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна.")]
        public string BirthDate { get; set; }
    }
}