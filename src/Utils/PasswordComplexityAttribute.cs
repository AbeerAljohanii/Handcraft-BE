using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ecommerce.src.Utils
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
                return false;

            if (password.Length < 8)
                return false;
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""\:{}|<>]");

            return hasLowerCase && hasUpperCase && hasDigit && hasSpecialChar;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Password must be at least 8 characters and contain uppercase, lowercase, a number and 1 special character ";
        }
    }
}
