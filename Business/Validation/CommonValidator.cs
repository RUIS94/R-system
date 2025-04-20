using System.Text.RegularExpressions;

namespace Business.Validation
{
    public static class CommonValidator
    {
        public static void ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} is required");
        }

        public static void ValidateUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required");

            if (username.Length < 3)
                throw new ArgumentException("Username must be at least 3 characters long");
        }

        public static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            if (!Regex.IsMatch(email, @"^\S+@\S+\.\S+$"))
                throw new ArgumentException("Invalid email format");
        }

        public static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (password.Length < 5)
                throw new ArgumentException("Password must be at least 5 characters long");
        }

        public static void ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone is required");

            if (!phone.All(char.IsDigit) || phone.Length > 10)
                throw new ArgumentException("Invalid phone number format");
        }

        public static void ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || address.Length < 5)
                throw new ArgumentException("Street address must be at least 5 characters long");
        }

        public static void ValidateDecimalRange(decimal value, string field, decimal min = 0, decimal max = decimal.MaxValue)
        {
            if (value < min || value > max)
                throw new ArgumentException($"{field} must be between {min} and {max}");
        }

        public static void ValidateStringLength(string value, string field, int min = 1, int max = 100)
        {
            if (value.Length < min || value.Length > max)
                throw new ArgumentException($"{field} must be between {min} and {max} characters");
        }
    }
}
