using System;
using System.Security.Cryptography;

namespace JCGAttendanceSystem.Security
{
    internal sealed class PasswordHashResult
    {
        public string HashBase64 { get; set; }
        public string SaltBase64 { get; set; }
    }

    internal static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 150000;

        public static PasswordHashResult HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash;
            using (var derive = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                hash = derive.GetBytes(HashSize);
            }

            return new PasswordHashResult
            {
                HashBase64 = Convert.ToBase64String(hash),
                SaltBase64 = Convert.ToBase64String(salt)
            };
        }

        public static bool Verify(string password, string saltBase64, string hashBase64)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(saltBase64) || string.IsNullOrEmpty(hashBase64))
                return false;

            try
            {
                var salt = Convert.FromBase64String(saltBase64);
                var expected = Convert.FromBase64String(hashBase64);
                byte[] actual;
                using (var derive = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    actual = derive.GetBytes(expected.Length);
                }
                return FixedTimeEquals(actual, expected);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
                return false;

            var difference = 0;
            for (var i = 0; i < left.Length; i++)
                difference |= left[i] ^ right[i];
            return difference == 0;
        }
    }
}
