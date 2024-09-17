using System.Security.Cryptography;

namespace PerformanceSurvey.Utilities
{
    public class PasswordGenerator
    {
        private static readonly char[] PasswordCharacters =
                    "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+[]{}|;:,.<>?".ToCharArray();

        public static string GenerateSecurePassword(int length = 12)
        {
            if (length < 8)
            {
                throw new ArgumentException("Password length should be at least 8 characters.");
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[length];
                rng.GetBytes(byteArray);

                var passwordChars = byteArray
                    .Select(b => PasswordCharacters[b % PasswordCharacters.Length])
                    .ToArray();

                return new string(passwordChars);
            }
        }
    }
}
