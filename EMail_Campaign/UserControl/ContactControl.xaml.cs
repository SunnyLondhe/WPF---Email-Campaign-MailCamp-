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

        ObservableCollection<Contact> groupContacts = new ObservableCollection<Contact>();

        public ContactControl()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                contacts = Storage.ReadFromXmlFile<ObservableCollection<Contact>>("DATA\\contacts.xml");
                fillFileList();
                //cboGroups.SelectedItem = "--Blank--";
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
        public void fillFileList()
        {
            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\DATA\\", "G*.xml");
            // cboGroups.Items.Add("--Blank--");
            foreach (string file in filePaths)
            {
                var fileName = System.IO.Path.GetFileName(file);
                cboGroups.Items.Add(fileName.Remove(0, 1).Replace(".xml", ""));

            }

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
                    Storage.WriteToXmlFile<ObservableCollection<Contact>>("DATA\\contacts.xml", contacts);
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
                            dateofbirth = splitLine[6].Trim(),

                        };
                                    
                        contacts.Add(oContact);

                    }
                    streamReader.Close();
                    Storage.WriteToXmlFile<ObservableCollection<Contact>>("DATA\\contacts.xml", contacts);
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
            var lst = from s in contacts orderby s.Name, s.City, s.Zip, s.State, s.dateofbirth, s.Gender select s;
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
                Storage.WriteToXmlFile<ObservableCollection<Contact>>("DATA\\contacts.xml", contacts);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtEmail.Text))
            {
                var con = new Contact { Name = txtName.Text.ToString(), City = txtCity.Text.ToString(), State = txtState.Text.ToString(), Zip = txtZip.Text.ToString(), Email = txtEmail.Text.ToString(), dateofbirth = txtdateofbirth.Text.ToString(), Gender = cboGender.SelectedItem.ToString() };
                contacts.Add(con);
                Storage.WriteToXmlFile<ObservableCollection<Contact>>("DATA\\contacts.xml", contacts);
                clearText();
            }
            else
            {
                MessageBox.Show("Please provide Name and Email-ID");
            }
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {


            if (cboGroups.SelectedItem == null)
            {
                if (string.IsNullOrEmpty(txt_GroupName.Text))
                {
                    MessageBox.Show("Please Enter Name for the Group");
                }
                else
                {
                    Storage.WriteToXmlFile<ObservableCollection<Contact>>("DATA\\G" + txt_GroupName.Text + ".xml", groupContacts);
                    groupContacts.Clear();
                    txt_GroupName.Clear();
                    cboGroups.Items.Clear();
                    fillFileList();
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? ", "You want to update the selected group", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Storage.WriteToXmlFile<ObservableCollection<Contact>>("DATA\\G" + cboGroups.SelectedItem.ToString() + ".xml", groupContacts);
                    groupContacts.Clear();
                    txt_GroupName.Clear();
                    cboGroups.Items.Clear();
                    fillFileList();
                }
            }

        }

        private void btnDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            groupContacts.Remove(lbx_ContGroup.SelectedItem as Contact);


        }

        private void btnAddtoGroup_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_Contact.SelectedItems.Count > 0)
            {
                List<Contact> selectedContacts = lbx_Contact.SelectedItems.Cast<Contact>().ToList();

                selectedContacts.ToList().ForEach(groupContacts.Add);
                lbx_ContGroup.ItemsSource = groupContacts;
                MessageBox.Show("Selected Items count is " + lbx_Contact.SelectedItems.Count);
            }
            else
            {
                MessageBox.Show("Please select contacts to add in group");

            }
        }

        private void btnDelGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? ", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\DATA\\G" + cboGroups.SelectedItem.ToString() + ".xml");
                    groupContacts.Clear();
                    txt_GroupName.Clear();
                    cboGroups.Items.Clear();
                    fillFileList();
                    MessageBox.Show("Group " + cboGroups.SelectedItem.ToString() + " Deleted Successfullty");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void cboGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboGroups.SelectedItem == null)
                {
                    groupContacts.Clear();
                    txt_GroupName.Clear();
                }
                else
                {

                    if (!string.IsNullOrEmpty(cboGroups.SelectedItem.ToString()))
                    {
                        groupContacts = Storage.ReadFromXmlFile<ObservableCollection<Contact>>("DATA\\G" + cboGroups.SelectedItem.ToString() + ".xml");
                        lbx_ContGroup.ItemsSource = groupContacts;
                    }
                    else
                    {
                        groupContacts.Clear();
                        txt_GroupName.Clear();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error : " + ex.Message);
            }

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;         
            double value = slider.Value;           
            TextElement.SetFontSize(ContactGrid, value);
        }

        private void txt_GroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Contains(" "))
            {
                MessageBox.Show("Your error message for users");
                tb.Clear();
                tb.Focus();

            }
        }
    }
}
