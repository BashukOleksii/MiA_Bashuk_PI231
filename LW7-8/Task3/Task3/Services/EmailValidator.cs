using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task3.Services
{
    public static class EmailValidator
    {
        public const string Standart = @"^[^\s@]+@[^\s@]+\.[^\s@]{2,}$";

        public static bool IsValidEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
                return false;
            return Regex.IsMatch(email,Standart);
        }

        public static string GetDomain(string email)
        {
            ValidateEmail(email);

            int index =  email.IndexOf('@');

            return email.Substring(index + 1).ToLower();

        }

        public static string SetNormalGmail(string email) 
        {
            ValidateEmail(email);

            string userPart = email.Substring(0, email.IndexOf('@'));

            userPart = userPart.Replace(".", "");

            int indexPlus = userPart.IndexOf('+');
            if (indexPlus >= 0)
                userPart = userPart.Substring(0, indexPlus);

            return $"{userPart.ToLower()}@gmail.com";

        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                throw new FormatException("Невірний формат електронної адреси");
        }

        
    }
}
