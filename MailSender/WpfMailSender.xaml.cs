using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }

        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            // Список адресов для рассылки.
            List<string> listStrMails = Constants.listStrMails;

            string strPassword = passwordBox.Password;
            string senderEmail = Constants.SENDER_EMAIL;

            string subject = tbSubject.Text;
            string body = tbBody.Text;

            foreach (string recieverEmail in listStrMails)
            {
                var mail = new EmailSendService();
                mail.Send(senderEmail, strPassword, recieverEmail, subject, body);
            }

            SendEndWindow sew = new SendEndWindow
            {
                Owner = this
            };
            sew.ShowDialog();
        }

    }
}