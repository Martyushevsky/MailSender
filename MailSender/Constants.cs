using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class Constants
    {
        public const string SENDER_EMAIL = "martynik@yandex.ru";

        public const string SUBJECT = "Привет из C#";

        public const string BODY = "Hello, world!";

        public static SmtpClientData smtpDataYandex = new SmtpClientData("smtp.yandex.ru", 25);

        public static List<string> listStrMails = new List<string> { "martyushevsky@gmail.com" };
    }

    public struct SmtpClientData
    {
        public string host;
        public int port;

        public SmtpClientData(string hostName, int portNumber)
        {
            host = hostName;
            port = portNumber;
        }
    }
}
