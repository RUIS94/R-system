using System.Text.RegularExpressions;

namespace Business.Validation
{
    public static class CommonValidator
    {
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
    }
}
