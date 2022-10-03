using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AmazyAuth.Tokens
{
    public class AuthOptions
    {
        public const string ISSUER = "AmazyAuthServer";
        public const string AUDIENCE = "AmazyAuthClient";
        public const string KEY = "AmazySecretKey123!!!GloryUkraine";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
