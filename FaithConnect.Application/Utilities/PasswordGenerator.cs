using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Utilities
{
    using System.Security.Cryptography;
    using System.Text;

    public static class PasswordGenerator
    {
        public static string Generate(
            int length = 12,
            bool includeUpper = true,
            bool includeLower = true,
            bool includeNumbers = true,
            bool includeSymbols = true)
        {
            var upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lower = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            var symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            var requiredSets = new List<string>();

            if (includeUpper) requiredSets.Add(upper);
            if (includeLower) requiredSets.Add(lower);
            if (includeNumbers) requiredSets.Add(numbers);
            if (includeSymbols) requiredSets.Add(symbols);

            if (!requiredSets.Any())
                throw new ArgumentException("At least one character set must be selected.");

            if (length < requiredSets.Count)
                throw new ArgumentException($"Password length must be at least {requiredSets.Count}.");

            var password = new List<char>();

            // Add one character from each required set
            foreach (var set in requiredSets)
            {
                password.Add(GetRandomChar(set));
            }

            // Build combined pool
            var allChars = string.Concat(requiredSets);

            while (password.Count < length)
            {
                password.Add(GetRandomChar(allChars));
            }

            // Shuffle
            Shuffle(password);

            return new string(password.ToArray());
        }

        private static char GetRandomChar(string chars)
        {
            return chars[RandomNumberGenerator.GetInt32(chars.Length)];
        }

        private static void Shuffle(IList<char> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}