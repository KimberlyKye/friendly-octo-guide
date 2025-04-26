using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dto
{
    public record UpdateStudentProfileRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Телефонный номер обязателен.")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Формат телефонного номера неверен.")]
        public string PhoneNumber { get; set; }
    }
}