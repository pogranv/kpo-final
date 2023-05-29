using System.Security.Cryptography;
using System.Text;

namespace Auth.Helpers;

public static class Hasher
{
    public static string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using (var hash = SHA256.Create())
        {
            var hashedBytes = hash.ComputeHash(bytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}