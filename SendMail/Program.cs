using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EMail_Campaign;

namespace SendMail
{
    class Program
    {
        static ObservableCollection<Setting> settings;
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a numeric argument.");

            }
            else
            {
                settings = Storage.ReadFromXmlFile<ObservableCollection<Setting>>("settings.xml");
                string to = "kapadearti@gmail.com";
                string from = "mailchamp@gmail.com";
                string subject = "Email From MailChamp";
                string body = @"This is an Test Email ";
                MailMessage message = new MailMessage(from, to, subject, body);
                SendMail(message);
            }
        }

        public static void SendMail(MailMessage Message)
        {

            string suserName = settings[0].useremail;
            string spassword = CryptPass.Decrypt(settings[0].password);
            SmtpClient client = new SmtpClient();
            client.Host = settings[0].host;
            client.Port = Convert.ToInt32(settings[0].port);
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(suserName, spassword);
            client.Send(Message);
        }
    }
}
