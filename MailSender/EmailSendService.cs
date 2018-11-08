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
        public EmailSendService() { }

        private string host = Constants.smtpDataYandex.host;
        private int port = Constants.smtpDataYandex.port;

        public void Send(string login, string password, string reciever, string subject, string body)
        {
            string senderEmail = login;
            string accountPassword = password;
            string recieverEmail = reciever;

            // Используем using, чтобы гарантированно удалить объект MailMessage после использования.
            using (MailMessage mm = new MailMessage(senderEmail, recieverEmail))
            {
                // Формируем письмо
                mm.Subject = subject;   // Заголовок письма.
                mm.Body = body;         // Тело письма.
                mm.IsBodyHtml = false;  // Не используем html в теле письма.

                // Авторизуемся на smtp-сервере и отправляем письмо.
                // Оператор using гарантирует вызов метода Dispose, даже если при вызове методов в объекте происходит исключение.
                using (SmtpClient sc = new SmtpClient(host, port))
                {
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential(senderEmail, accountPassword);

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
        }
    }
}