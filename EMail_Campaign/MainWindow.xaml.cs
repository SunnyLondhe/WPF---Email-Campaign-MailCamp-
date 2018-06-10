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
        }


        private void ContactMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Grd_MainView.Children.Count >0)
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

        }

        private void SettingMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Grd_MainView.Children.Count > 0)
                Grd_MainView.Children.RemoveAt(0);
            UserControl.Settings C_Setting = new UserControl.Settings();

            Grd_MainView.Children.Add(C_Setting);

        }
    }
}
