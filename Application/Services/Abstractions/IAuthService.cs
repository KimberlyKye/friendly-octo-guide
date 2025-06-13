using System.Security.Claims;

namespace Application.Services.Abstractions;

public interface IAuthService
{
    public Task<ClaimsIdentity> GetIdentity(string email, string password);

    public string GetEncodedToken(IEnumerable<Claim> claims);
}