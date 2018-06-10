using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings 
    {
        ObservableCollection<Setting> settings;
        public Settings()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                settings = Storage.ReadFromXmlFile<ObservableCollection<Setting>>("settings.xml");
                this.DataContext = settings;
                txtPassword.Password =settings[0].password;
                          }
            catch (Exception ex)
            {
                //throw;
            }

        }

        private void btn_SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            if (settings.Count < 1)
            {
                var oSet = new Setting { host = txtHostName.Text, password = txtPassword.Password.ToString(), port = txtPort.Text, useremail = txtEmailID.Text };
                settings.Add(oSet);

            }
            else {
                settings[0].password = CryptPass.Crypt( txtPassword.Password);
                Storage.WriteToXmlFile<ObservableCollection<Setting>>("settings.xml", settings);
            }
        }
    }
}
