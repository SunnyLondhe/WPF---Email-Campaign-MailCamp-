using System;
using System.Collections.Generic;
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


namespace EMail_Campaign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateFiles();
        }
        public void CreateFiles()
        {
            if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + "\\DATA"))
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DATA");
            string contactsFile = Directory.GetCurrentDirectory() + "\\DATA\\contacts.xml";
            string TemplatesFile = Directory.GetCurrentDirectory() + "\\DATA\\Templates.xml";
            string settingsFile = Directory.GetCurrentDirectory() + "\\DATA\\settings.xml";
            string ErrorLogFile = Directory.GetCurrentDirectory() + "\\DATA\\ErrorLog.xml";
            string EmailSetLogFile = Directory.GetCurrentDirectory() + "\\DATA\\EmailSentLog.xml";
            //DATA\\contacts.xml
            if (!(File.Exists(contactsFile)))
                File.CreateText(contactsFile);


            if (!(File.Exists(TemplatesFile)))
                File.CreateText(TemplatesFile);

            if (!(File.Exists(settingsFile)))
                File.CreateText(settingsFile);

            if (!(File.Exists(ErrorLogFile)))
                File.CreateText(ErrorLogFile);


            if (!(File.Exists(EmailSetLogFile)))
                File.CreateText(EmailSetLogFile);




        }


        private void ContactMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Grd_MainView.Children.Count > 0)
                Grd_MainView.Children.RemoveAt(0);
            UserControl.ContactControl C_Control = new UserControl.ContactControl();

            Grd_MainView.Children.Add(C_Control);

        }

        private void TemplateMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Grd_MainView.Children.Count > 0)
                Grd_MainView.Children.RemoveAt(0);
            UserControl.Theme C_Theme = new UserControl.Theme();

            Grd_MainView.Children.Add(C_Theme);

        }

        private void DashboardMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Grd_MainView.Children.Count > 0)
                Grd_MainView.Children.RemoveAt(0);
            UserControl.Dashboard C_Dashboard = new UserControl.Dashboard();

            Grd_MainView.Children.Add(C_Dashboard);
        }

        private void SettingMenu_Click(object sender, RoutedEventArgs e)
        {
            Settings wSetting = new Settings();

            wSetting.Show();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserControl.Dashboard C_Dashboard = new UserControl.Dashboard();
            Grd_MainView.Children.Add(C_Dashboard);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
