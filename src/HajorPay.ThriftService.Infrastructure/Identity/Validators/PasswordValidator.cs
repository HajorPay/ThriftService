using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Infrastructure.Identity.Validators
{
    public class PasswordValidator
    {
        public static bool IsPasswordValid(string password, string username, string email, string appName, string domain)
        {
            // Check for minimum length
            if (password.Length < 8)
                return false;

            // Check for common passwords (use a list or API)
            var commonPasswords = new HashSet<string> { "password", "123456", "qwerty" };
            if (commonPasswords.Contains(password))
                return false;

            // Check for special cases
            if (password.Equals(username, StringComparison.OrdinalIgnoreCase) ||
                password.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                password.Equals(appName, StringComparison.OrdinalIgnoreCase) ||
                password.Equals(domain, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Basic entropy check (e.g., at least 3 unique character types)
            var uniqueCharTypes = new HashSet<char>();
            foreach (var c in password)
            {
                if (char.IsLower(c)) uniqueCharTypes.Add('L');
                if (char.IsUpper(c)) uniqueCharTypes.Add('U');
                if (char.IsDigit(c)) uniqueCharTypes.Add('D');
                if (char.IsSymbol(c) || char.IsPunctuation(c)) uniqueCharTypes.Add('S');
            }
            if (uniqueCharTypes.Count < 3)
                return false;

            return true;
        }
    }
}
