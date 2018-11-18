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

            cbSenderSelect.ItemsSource = Variables.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            cbSmtpSelect.ItemsSource = Variables.Servers;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";

            DB db = new DB();
            dataGrid.ItemsSource = db.Emails;
        }

        //private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        //{
        //    // Пример вызова пользовательского окна.
        //    SendEndWindow sew = new SendEndWindow
        //    {
        //        Owner = this
        //    };
        //    sew.ShowDialog();
        //}

        private void MiClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tiPlanner;
        }

        private void BtnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string login = cbSenderSelect.Text;
            string password = cbSenderSelect.SelectedValue.ToString();

            string server = cbSmtpSelect.Text;
            int port = (int)cbSmtpSelect.SelectedValue;

            string emailSubject = tbSubject.Text;
            string emailBody = new TextRange(tbBody.Document.ContentStart, tbBody.Document.ContentEnd).Text;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }

            if (string.IsNullOrWhiteSpace(emailBody))
            {
                MessageBox.Show("Письмо не заполнено");
                tabControl.SelectedItem = tiEmailEditor;
                return;
            }

            EmailSendService emailSender = new EmailSendService(login, password, server, port, emailSubject, emailBody);
            emailSender.SendMails((IQueryable<Email>)dataGrid.ItemsSource);
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string login = cbSenderSelect.Text;
            string password = cbSenderSelect.SelectedValue.ToString();

            string server = cbSmtpSelect.Text;
            int port = (int)cbSmtpSelect.SelectedValue;

            string emailSubject = tbSubject.Text;
            string emailBody = new TextRange(tbBody.Document.ContentStart, tbBody.Document.ContentEnd).Text;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }

            if (!string.IsNullOrWhiteSpace(emailBody))
            {
                MessageBox.Show("Письмо не заполнено");
                tabControl.SelectedItem = tiEmailEditor;
                return;
            }

            //TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            TimeSpan tsSendTime = TimeSpan.Parse(tpTimePicker.Value.ToString());
            if (tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }

            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);

            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }

            SchedulerClass sc = new SchedulerClass();
            EmailSendService emailSender = new EmailSendService(login, password, server, port, emailSubject, emailBody);
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dataGrid.ItemsSource);
        }
    }
}