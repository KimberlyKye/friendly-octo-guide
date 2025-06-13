using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Dto.Auth;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var identity = await _authService.GetIdentity(loginRequest.Email, loginRequest.Password);
        if (identity == null)
        {
            return BadRequest(new { errorText = " Invalid username or password" });
        }

        var encodedJwt = _authService.GetEncodedToken(identity.Claims);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name,
            role = identity.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value
        };

        return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
    {
        var identity = await _authService.GetIdentity(registerRequest.Email, registerRequest.Password);
        if (identity == null)
        {
            return BadRequest(new { errorText = " Invalid username or password" });
        }


        return Ok();
    }

    private IAuthService _authService;
}