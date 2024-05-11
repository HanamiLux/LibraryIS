using System.Security.Cryptography;
using System.Text;

namespace LibraryWebApi.Models
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltBytes = new byte[32];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }

                var salt = Convert.ToBase64String(saltBytes);
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(saltedPassword);
                return $"{salt}{Convert.ToBase64String(hashBytes)}";
            }
        }
    }
}
