using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    /// <summary>
    /// Класс, который отвечает за работу с базой данных
    /// </summary>
    class DB
    {
        private EmailsDataContext emails = new EmailsDataContext();

        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.Emails select c;
            }
        }
    }
}
