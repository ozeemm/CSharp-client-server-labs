using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lab2._JWT
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // Издатель токена
        public const string AUDIENCE = "MyAuthClient"; // Потребитель токена
        const string KEY = "supersecretkeysupersecretkeysupersecretkey";   // Ключ для шифрации
        public static TimeSpan LIFETIME = TimeSpan.FromMinutes(2); // Время жизни
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
