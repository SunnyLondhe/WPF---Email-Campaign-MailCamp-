using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        static ObservableCollection<Contact> groups;
        static ObservableCollection<Template> templates;
        static ObservableCollection<Errorlog> errorlogs = new ObservableCollection<Errorlog>();
        static ObservableCollection<EmailSentlog> emailsentlogs = new ObservableCollection<EmailSentlog>();
        static string sGroupName;
        static string sTemplateName;
        static string sContactName;
        static string sEmail;

        static void Main(string[] args)
        {
            //checkLogFiles();
            WriteErrorLog("Information", "Email Sent Started");
            Console.WriteLine("Mail Camp | Email Sent Started");
            try
            {

            
            if (args.Length == 0)
            {
                
                WriteErrorLog("Warning", "Parameters not provided");
                Console.WriteLine("Parameters not provided");
            }
            else
            {
                if (args.Length == 3)
                {
                    Console.WriteLine("Scheduled" + args[0].Replace("\\\\DATA\\\\", "").Remove(0, 1).Replace(".xml", ""));
                    Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
                    sGroupName = args[0].Replace("DATA\\", "").Remove(0, 1).Replace(".xml", "");
                    sTemplateName = args[1];
                    Console.WriteLine(args[0] +" | "+ args[1] + " | " + args[2]);
                    settings = Storage.ReadFromXmlFile<ObservableCollection<Setting>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\settings.xml");
                    groups = Storage.ReadFromXmlFile<ObservableCollection<Contact>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\G"+sGroupName+".xml");// this is to be changed
                    templates = Storage.ReadFromXmlFile<ObservableCollection<Template>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\Templates.xml");
                        if (!System.IO.Directory.Exists(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA"))
                            System.IO.Directory.CreateDirectory(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA");

                        string ErrorLogFile = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\ErrorLog.xml";
                        string EmailSetLogFile = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\EmailSentLog.xml";
                        if (!(File.Exists(ErrorLogFile)))
                        {
                            File.CreateText(ErrorLogFile);

                        }
                        if (!(File.Exists(EmailSetLogFile)))
                        {
                            File.CreateText(EmailSetLogFile);

                        };
                        SendMail(args[1]);
                    Console.WriteLine(args[0] + " | " + args[1] + " | " + args[2]);
                }else if (args.Length == 2)
                {
                    Console.WriteLine("Manual");
                    sGroupName = args[0].Replace("DATA\\", "").Remove(0, 1).Replace(".xml", "");
                    sTemplateName = args[1];
                    settings = Storage.ReadFromXmlFile<ObservableCollection<Setting>>(Directory.GetCurrentDirectory() + "\\DATA\\settings.xml");
                    groups = Storage.ReadFromXmlFile<ObservableCollection<Contact>>(Directory.GetCurrentDirectory() + "\\DATA\\G"+sGroupName+".xml");// this is to be changed
                    templates = Storage.ReadFromXmlFile<ObservableCollection<Template>>(Directory.GetCurrentDirectory() + "\\DATA\\Templates.xml");
                        if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + "\\DATA"))
                            System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DATA");

                        string ErrorLogFile = Directory.GetCurrentDirectory() + "\\DATA\\ErrorLog.xml";
                        string EmailSetLogFile = Directory.GetCurrentDirectory() + "\\DATA\\EmailSentLog.xml";
                        if (!(File.Exists(ErrorLogFile)))
                        {
                            File.CreateText(ErrorLogFile);

                        }
                        if (!(File.Exists(EmailSetLogFile)))
                        {
                            File.CreateText(EmailSetLogFile);

                        };
                        SendMail(args[1]);
                }
                else
                {
                    WriteErrorLog("Warning", "Invalid Arguments Provided");
                }

            }
            WriteErrorLog("Information", "Email Sent Completed");
            Console.WriteLine("Mail Camp | Email Sent Completed");
            Storage.WriteToXmlFile<ObservableCollection<Errorlog>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\ErrorLog.xml", errorlogs, true);
            Storage.WriteToXmlFile<ObservableCollection<EmailSentlog>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\EmailSentLog.xml", emailsentlogs, true);
            }
            catch (Exception ex)
            {
                WriteErrorLog("Error", "Error -" + ex.Message);
                Console.WriteLine("ERROR - " + ex.Message);
                Storage.WriteToXmlFile<ObservableCollection<Errorlog>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\ErrorLog.xml", errorlogs, true);
                Storage.WriteToXmlFile<ObservableCollection<EmailSentlog>>(System.Reflection.Assembly.GetEntryAssembly().Location.Replace("SendMail.exe", "") + "DATA\\EmailSentLog.xml", emailsentlogs,true);
            }
        }

        private static void checkLogFiles()
        {
            if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + "\\DATA"))
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DATA");

            string ErrorLogFile = Directory.GetCurrentDirectory() + "\\DATA\\ErrorLog.xml";
            string EmailSetLogFile = Directory.GetCurrentDirectory() + "\\DATA\\EmailSentLog.xml";
            if (!(File.Exists(ErrorLogFile)))
            {
                File.CreateText(ErrorLogFile);

            }
            if (!(File.Exists(EmailSetLogFile)))
            {
                File.CreateText(EmailSetLogFile);

            };
        }

        public static void SendMail(string TemplateName)
        {
            try
            {
                string suserName = settings[0].useremail;
                MailAddress fromMail = new MailAddress(suserName);

                var subject = templates.Where(x => x.TemplateName == TemplateName).Select(x => x.Subject).ToList();
                var body = templates.Where(x => x.TemplateName == TemplateName).Select(x => x.EmailContext).ToList();
                MailMessage message = new MailMessage();
                message.From = fromMail;
                message.Subject = subject[0];
                SmtpClient client = new SmtpClient();
                client.Host = settings[0].host;
                client.Port = Convert.ToInt32(settings[0].port);
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                string spassword = CryptPass.Decrypt(settings[0].password);
                client.Credentials = new NetworkCredential(suserName, spassword);
                foreach (var ocontacts in groups)
                {
                    sContactName = ocontacts.Name;
                    sEmail = ocontacts.Email;
                    message.To.Add(ocontacts.Email);
                    message.Body = body[0].Replace("<Name>", ocontacts.Name);
                    client.Send(message);

                    WriteEmailLog(sContactName, sEmail, "Sent");
                    message.To.Clear();
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                WriteEmailLog(sContactName, sEmail, "SmtpFailed Error", ex.Message.ToString());

            }
            catch (SmtpException ex)
            {
                WriteEmailLog(sContactName, sEmail, "SmtpException Error", ex.Message.ToString());
            }
            catch(Exception ex) {

                WriteErrorLog("Critical", ex.Message.ToString());
            }
        }

        public static void WriteErrorLog(string sErrorType, string sLogDiscription ) // Type Critical, Warning, Information
        { 
            var con = new Errorlog { logtime = DateTime.Now.ToString(), ErrorType = sErrorType, groupname = sGroupName, templateName = sTemplateName, logDiscription = sLogDiscription };
            errorlogs.Add(con);

        }
        public static void WriteEmailLog(string sContactName, string sEmail,string sStatus,string sLogMessage = "")
        {
            var con = new EmailSentlog { logtime = DateTime.Now.ToString(), contactname = sContactName, email= sEmail, status= sStatus, message= sLogMessage };
            emailsentlogs.Add(con);

        }
    }
}
