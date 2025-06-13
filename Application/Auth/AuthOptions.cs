using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth;

public class AuthOptions
{
    public const string ISSUER = "Service";
    public const string AUDIENCE = "Client";
    public const string KEY = "795379D4-B475-42B8-8E47-CC285F112C4C";
    public const int LIFETIME = 480;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}