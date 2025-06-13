using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Auth;
using Application.Services.Abstractions;
using Entities;
using Infrastructure.Repositories.Abstractions;
using Microsoft.IdentityModel.Tokens;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services;

public class AuthService : IAuthService
{
    public async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !VerifyPassword(password, user.Password))
        {
            return null;
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString() ),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        return claimsIdentity;
    }

    public string GetEncodedToken(IEnumerable<Claim> claims)
    {
        var now =  DateTime.UtcNow ;

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private static bool VerifyPassword(string password, string hashedPassword)
    {
        var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
        using (var sha256 = SHA256.Create())
        {
            var computedHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hashedPasswordBytes.SequenceEqual(computedHashBytes);
        }
    }

    private IUserRepository _userRepository;

    private IUserProfileRepository<Student> _userProfileRepository;
}