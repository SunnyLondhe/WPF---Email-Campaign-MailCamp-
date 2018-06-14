using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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


namespace EMail_Campaign.UserControl
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard
    {
        ObservableCollection<Contact> groupContacts = new ObservableCollection<Contact>();
        ObservableCollection<Template> templates = new ObservableCollection<Template>();
         ObservableCollection<Errorlog> errorlogs = new ObservableCollection<Errorlog>();
         ObservableCollection<EmailSentlog> emailsentlogs = new ObservableCollection<EmailSentlog>();
        public Dashboard()
        {
            InitializeComponent();
            fillGroups();
            fillTemplate();
            fillLogData();
            string s = DateTime.Now.ToString("HH:mm:ss");
            DateTime dt = DateTime.Now;
            
            txtStartTime.Text = s;
            txtSchDatePick.Text = DateTime.Now.ToString("MM/dd/yyyy");

        }
        public void fillLogData() {
            
            emailsentlogs = Storage.ReadFromXmlFile<ObservableCollection<EmailSentlog>>("DATA\\EmailSentLog.xml");
            lbx_EmailSent.ItemsSource = emailsentlogs;
            errorlogs = Storage.ReadFromXmlFile<ObservableCollection<Errorlog>>("DATA\\ErrorLog.xml");
            lbx_ErrorLog.ItemsSource = errorlogs;
        }
        public void fillGroups()
        {
            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\DATA\\", "G*.xml");
            // cboGroups.Items.Add("--Blank--");
            cboConGroup.Items.Add("Select Contact Group");

            foreach (string file in filePaths)
            {
                var fileName = System.IO.Path.GetFileName(file);
                cboConGroup.Items.Add(fileName.Remove(0, 1).Replace(".xml", ""));
                cboSchConGroup.Items.Add(fileName.Remove(0, 1).Replace(".xml", ""));
            }

        }
        public void fillTemplate()
        {

            templates = Storage.ReadFromXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml");
            cboTemplate.ItemsSource = templates;
            cboSchTemplate.ItemsSource = templates;

            //cboTemplate.Items.Add("Select Template");
            //cboTemplate.SelectedItem = "Select Template";


        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\SendMail.exe", "DATA\\G" + cboConGroup.SelectedItem + ".xml" + " " + ((EMail_Campaign.Template)cboTemplate.SelectedItem).TemplateName);
        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\SendMail.exe";
            string taskArgument = "/c schtasks /create /tn \"MailCamp-"+txtTaskName.Text+"\" /tr \"'" + path + "' DATA\\G" + cboSchConGroup.SelectedItem + ".xml" + " " + ((EMail_Campaign.Template)cboSchTemplate.SelectedItem).TemplateName + " 1 \" /sc " + ((System.Windows.Controls.ContentControl)cboTaskTrigger.SelectedValue).Content + " /st " + txtStartTime.Text.ToString() + " /sd " + txtSchDatePick.Text + " /ru \"SYSTEM\"";

            
             Process.Start("C:\\WINDOWS\\system32\\cmd.exe", taskArgument);
        }

        private void btnWinSchedular_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\WINDOWS\\system32\\cmd.exe", "/c taskschd.msc");
        }
    }
}
