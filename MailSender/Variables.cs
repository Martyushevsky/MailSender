using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePasswordDLL;

namespace MailSender
{
    public static class Variables
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }

        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            { "79257443993@yandex.ru", CodePassword.GetDecryptedPassword("qbttxpse") },
            { "sok74@yandex.ru", CodePassword.GetDecryptedPassword("qbspm(") }
        };

        public static Dictionary<string, int> Servers
        {
            get { return dicServers; }
        }

        private static Dictionary<string, int> dicServers = new Dictionary<string, int>()
        {
            { "smtp.yandex.ru", 25 }
        };
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
