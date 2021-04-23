using System;
using System.Text;

namespace FoodOrder.Application.Utils
{
    public class PasswordEncrypter
    {
        public static string Encrypt(string password)
        {
            var textBytest= Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(textBytest);
        }
    }
}