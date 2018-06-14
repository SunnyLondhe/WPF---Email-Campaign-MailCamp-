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
    /// Interaction logic for Theme.xaml
    /// </summary>
    public partial class Theme
    {
        ObservableCollection<Template> templates = new ObservableCollection<Template>();
        public Theme()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                
                templates = Storage.ReadFromXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml");
               // btnSave.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //throw;
            }

            //  setGenderCbo();
            templates = Storage.ReadFromXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml");
            lbx_Template.ItemsSource = templates;
            //ListView1.ItemsSource = contacts;
        }
        private void tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
             var lst = from s in templates where s.TemplateName.ToLower().Contains(tbx_filter.Text.ToLower()) || s.Subject.ToLower().Contains(tbx_filter.Text.ToLower()) select s;
            lbx_Template.ItemsSource = lst;
        }
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? ", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (lbx_Template.SelectedItem == null)
                        MessageBox.Show("Pelase Select Item to Delete");
                    else
                    {
                        int itemselectCount = lbx_Template.SelectedItems.Count;
                        for (int i = 0; i < itemselectCount; i++)
                        {

                            templates.Remove(lbx_Template.SelectedItems[0] as Template);
                            counter++;
                        }
                    }
                    Storage.WriteToXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml", templates);
                    MessageBox.Show("Deleted " + counter + " items");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }

            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            clearText();
          //  btnSave.IsEnabled = true;
        }
        private void clearText()
        {
            txtTemplateName.Clear();
            txtSubject.Clear();
            // txtRichText.Document.Blocks.Clear();
            txtRichText.SelectAll();

            txtRichText.Selection.Text = "";
            lbx_Template.SelectedItems.Clear();
            



        }
        private void btn_Sort_Click(object sender, RoutedEventArgs e)
        {
            //var lst = from s in contacts orderby s.Name, s.City, s.Zip, s.State, s.Age, s.Gender select s;
            //lbx_Contact.ItemsSource = lst;
        }



        private void Btn_Help_Click(object sender, RoutedEventArgs e)
        {
            // Help win = new Help();
            //  win.Owner = this;
            //win.ShowDialog();
        }

        private void btn_DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? All contacts will be deleted", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                templates.Clear();
                Storage.WriteToXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml", templates);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var EmailTemplateText = new TextRange(txtRichText.Document.ContentStart, txtRichText.Document.ContentEnd);
            
            // return textRange.Text;
            if (!templates.Any(str => str.TemplateName.Contains(txtTemplateName.Text.ToString())))
            {
                var Temp = new Template { TemplateName = txtTemplateName.Text.ToString(), Subject = txtSubject.Text.ToString(), EmailContext = EmailTemplateText.Text.ToString() };

                templates.Add(Temp);
                Storage.WriteToXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml", templates);
            }
            else {
                Storage.WriteToXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml", templates);
            }
            clearText();
            templates = Storage.ReadFromXmlFile<ObservableCollection<Template>>("DATA\\Templates.xml");
            lbx_Template.ItemsSource = templates;

        }
    }
}

