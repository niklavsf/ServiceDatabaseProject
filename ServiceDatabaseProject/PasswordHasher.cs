using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ServiceDatabaseProject
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            int iterations = 100000;
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return "PBKDF2$" + iterations.ToString() + "$" +
                       Convert.ToBase64String(salt) + "$" +
                       Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string stored)
        {
            if (string.IsNullOrWhiteSpace(stored)) return false;

            string[] parts = stored.Split('$');
            if (parts.Length != 4) return false;
            if (parts[0] != "PBKDF2") return false;

            int iterations;
            bool ok = int.TryParse(parts[1], out iterations);
            if (!ok) return false;

            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] expected = Convert.FromBase64String(parts[3]);

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256))
            {
                byte[] actual = pbkdf2.GetBytes(32);
                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
        }

        public static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%";
            char[] buffer = new char[length];

            using (System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                byte[] data = new byte[length];
                rng.GetBytes(data);

                for (int i = 0; i < length; i++)
                {
                    int index = data[i] % chars.Length;
                    buffer[i] = chars[index];
                }
            }

            return new string(buffer);
        }
    }

}
