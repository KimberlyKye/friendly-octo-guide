using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Auth;

public class LoginRequestDto
{
    /// <summary>
    /// Email
    /// </summary>
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [Required(ErrorMessage = "Пароль обязателен")]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }
}