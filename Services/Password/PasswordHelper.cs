using System.Security.Cryptography;
using System.Text;

namespace ConstradeApi.Services.Password
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                string hashString = Convert.ToBase64String(hash);

                return hashString;
            }
        }
    }
}
