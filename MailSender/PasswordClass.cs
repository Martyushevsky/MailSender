using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class PasswordClass
    {
        /// <summary>
        /// Расшифровывает зашифрованный пароль
        /// </summary>
        /// <param name="encryptedPassword">Зашифрованный пароль</param>
        /// <returns>Расшифрованный пароль</returns>
        public static string GetPassword(string encryptedPassword)
        {
            string password = "";
            foreach (char a in encryptedPassword)
            {
                char ch = a;
                ch--;
                password += ch;
            }
            return password;
        }

        /// <summary>
        /// Зашифровывает пароль
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Зашифрованный пароль</returns>
        public static string GetEncryptedPassword(string password)
        {
            string encryptedPassword = "";
            foreach (char a in password)
            {
                char ch = a;
                ch++;
                encryptedPassword += ch;
            }
            return encryptedPassword;
        }
    }
}
