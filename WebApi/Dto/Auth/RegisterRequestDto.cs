namespace WebApi.Dto.Auth;

public class RegisterRequestDto
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    public string Password { get; set; }

    /// <summary>
    /// First Name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateOnly DateOfBirth { get; set; }
}