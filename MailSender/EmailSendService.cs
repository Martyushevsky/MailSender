using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace MailSender
{
    public class EmailSendService
    {
        #region vars
        private string _login;      // email, c которого будет рассылаться почта
        private string _password;   // пароль к email, с которого будет рассылаться почта
        private string _smtpHost;   // smtp - server
        private int _smtpPort;      // порт для smtp-server
        private string _subject;    // тема письма для отправки
        private string _body;       // текст письма для отправки
        #endregion

        public EmailSendService(string login, string password, string server, int port, string subject, string body)
        {
            _login = login;
            _password = password;
            _smtpHost = server;
            _smtpPort = port;
            _subject = subject;
            _body = body;
        }

        // Отправка email конкретному адресату
        private void SendMail(string sendTo, string name)
        {
            using (MailMessage mm = new MailMessage(_login, sendTo))
            {
                mm.Subject = _subject;
                mm.Body = _body;
                mm.IsBodyHtml = false;

                SmtpClient sc = new SmtpClient(_smtpHost, _smtpPort)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_login, _password)
                };

                try
                {
                    sc.Send(mm);
                }
                catch (SmtpException error)
                {
                    SmtpError se = new SmtpError();
                    se.lSmtpError.Content = error.Message;
                    se.ShowDialog();
                }
            }
        }

        // Отправка email всем адресатам
        public void SendMails(IQueryable<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendMail(email.Value, email.Name);
            }
        }
    }
}