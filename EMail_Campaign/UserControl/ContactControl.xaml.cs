using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Serialization;

namespace EMail_Campaign.UserControl
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl
    {
        ObservableCollection<Contact> contacts;

        public ContactControl()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                contacts = Storage.ReadFromXmlFile<ObservableCollection<Contact>>("contacts.xml");
            }
            catch (Exception ex)
            {
                throw;
            }

            setGenderCbo();
            lbx_Contact.ItemsSource = contacts;
            //ListView1.ItemsSource = contacts;
        }

        private void setGenderCbo()
        {
            ObservableCollection<string> ocGender = new ObservableCollection<string>();
            ocGender.Add("Male");
            ocGender.Add("Female");
            //ocGender.Add("Other");
            cboGender.ItemsSource = ocGender;
            cboGender.SelectedItem = "Male";


        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? ", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (lbx_Contact.SelectedItem == null)
                        MessageBox.Show("Pelase Select Item to Delete");
                    else
                    {
                        int itemselectCount = lbx_Contact.SelectedItems.Count;
                        for (int i = 0; i < itemselectCount; i++)
                        {

                            contacts.Remove(lbx_Contact.SelectedItems[0] as Contact);
                            counter++;
                        }
                    }
                    Storage.WriteToXmlFile<ObservableCollection<Contact>>("contacts.xml", contacts);
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
        }
        private void clearText()
        {
            txtName.Clear();
            txtAge.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtZip.Clear();
            txtEmail.Clear();
      


        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {//txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                using (var streamReader = new StreamReader(openFileDialog.FileName))
                {
                    // browse the csv file line by line until the end of the file
                    while (!streamReader.EndOfStream)
                    {

                        // for each line, split it with the split caractere (that may no be ';')
                        var splitLine = streamReader.ReadLine().Split(',');

                        if (splitLine[0].Trim() == "Name" && splitLine[1].Trim() == "Email" && splitLine[2].Trim() == "City" && splitLine[3].Trim() == "State" && splitLine[4].Trim() == "Zip" && splitLine[5].Trim() == "Gender" && splitLine[6].Trim() == "Age")
                        {
                            continue;
                        }
                        count++;
                        Contact oContact = new Contact()
                        {
                            Name = splitLine[0].Trim(),
                            Email = splitLine[1].Trim(),
                            City = splitLine[2].Trim(),
                            State = splitLine[3].Trim(),
                            Zip = splitLine[4].Trim(),
                            Gender = splitLine[5].Trim(),
                            Age = splitLine[6].Trim(),

                        };


                        contacts.Add(oContact);

                    }
                    streamReader.Close();
                    Storage.WriteToXmlFile<ObservableCollection<Contact>>("contacts.xml", contacts);
                    MessageBox.Show("Updated " + count + "  contacts");
                }
            }
        }
        private void tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var lst = from s in contacts where s.Name.ToLower().StartsWith(tbx_filter.Text.ToLower()) || s.City.ToLower().StartsWith(tbx_filter.Text.ToLower()) || s.Zip.ToLower().StartsWith(tbx_filter.Text.ToLower()) || s.State.ToLower().StartsWith(tbx_filter.Text.ToLower()) || s.Gender.ToLower().StartsWith(tbx_filter.Text.ToLower()) || s.Email.ToLower().Contains(tbx_filter.Text.ToLower()) select s;
            lbx_Contact.ItemsSource = lst;
        }

        private void btn_Sort_Click(object sender, RoutedEventArgs e)
        {
            var lst = from s in contacts orderby s.Name, s.City, s.Zip, s.State, s.Age, s.Gender select s;
            lbx_Contact.ItemsSource = lst;
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
                contacts.Clear();
                Storage.WriteToXmlFile<ObservableCollection<Contact>>("contacts.xml", contacts);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var con = new Contact { Name = txtName.Text.ToString(), City = txtCity.Text.ToString(), State = txtState.Text.ToString(), Zip = txtZip.Text.ToString(), Email = txtEmail.Text.ToString(), Age = txtAge.Text.ToString(), Gender = cboGender.SelectedItem.ToString() };
            contacts.Add(con);
            Storage.WriteToXmlFile<ObservableCollection<Contact>>("contacts.xml", contacts);
        }
    }
}
