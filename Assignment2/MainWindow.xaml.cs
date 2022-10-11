using Assignment2.Models;
using Assignment2.Services;
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

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contact> contacts;
        private string filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\contacts2.json"; //name of file the contacts get stored in
        FileManager fileService;

        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<Contact>();
            fileService = new FileManager(filePath);
            GetContacts(); // call getcontacts, if no file exists one will be created
            lv_Contacts.ItemsSource = contacts;
            btn_Update.Visibility = Visibility.Hidden; // have update and back button as hidden 
            btn_Back.Visibility = Visibility.Hidden;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e) //adds contact to list and file
        {
            
            if (tb_FirstName.Text != "" && tb_LastName.Text != "") // require user to input first and last name to create a contact
            {
                var contactExist = contacts.FirstOrDefault(x => x.Email == tb_Email.Text); // check if contact already exists by email as unique id
                if (contactExist == null)
                {
                   
                    var contact = new Contact();
                    contact.FirstName = tb_FirstName.Text;
                    contact.LastName = tb_LastName.Text;
                    contact.Email = tb_Email.Text;
                    contact.Phone = tb_Phone.Text;
                    contact.Address = tb_Address.Text;
                    contact.ZipCode = tb_Zip.Text;
                    contact.City = tb_City.Text;
                    contacts.Add(contact);
                    fileService.Save(contacts);

                }
                else
                {
                    MessageBox.Show("Contact with given email already exists");
                }
            } else
            {
                MessageBox.Show("Contact needs a first and last name to be added to list");
            }
            ClearText();
        }

        private void ClearText() // simple method to clear text fields
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_Phone.Text = "";
            tb_Address.Text = "";
            tb_Zip.Text = "";
            tb_City.Text = "";
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e) // deletes contact, saves list to file and clear text fields
        {
            var button = sender as Button;
            var contact = (Contact)button!.DataContext;
            contacts.Remove(contact);
            fileService.Save(contacts);
            ClearText();
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // selects contact and shows information, hides add button and shows update & back button
            var contact = (Contact)lv_Contacts.SelectedItem;
            if (contact != null)
            {
                tb_FirstName.Text = contact.FirstName;
                tb_LastName.Text = contact.LastName;
                tb_Email.Text = contact.Email;
                tb_Phone.Text = contact.Phone;
                tb_Address.Text = contact.Address;
                tb_Zip.Text = contact.ZipCode;
                tb_City.Text = contact.City;
                btn_Add.Visibility = Visibility.Hidden;
                btn_Update.Visibility = Visibility.Visible;
                btn_Back.Visibility = Visibility.Visible;
            } 
            else
            {
                btn_Add.Visibility = Visibility.Visible;
                btn_Update.Visibility = Visibility.Hidden;
                btn_Back.Visibility = Visibility.Hidden;
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e) //updates information in contact
        {
            var contact = (Contact)lv_Contacts.SelectedItem;
            contact.FirstName = tb_FirstName.Text;
            contact.LastName = tb_LastName.Text;
            contact.Email = tb_Email.Text;
            contact.Phone = tb_Phone.Text;
            contact.Address = tb_Address.Text;
            contact.ZipCode = tb_Zip.Text;
            contact.City = tb_City.Text;
            lv_Contacts.UnselectAll(); // unselect item
            ClearText();
            fileService.Save(contacts);
            lv_Contacts.Items.Refresh(); // refresh listview items so new information gets visible immediately  
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e) //button to go back to 'main menu' after selecting a contact if not wish to update
        {
            ClearText();
            btn_Add.Visibility = Visibility.Visible;
            btn_Back.Visibility = Visibility.Hidden;
            btn_Update.Visibility = Visibility.Hidden;
            lv_Contacts.UnselectAll();
        }

        private ObservableCollection<Contact> GetContacts() //method to get contacts from jsonfile
        {
            fileService.Read(ref contacts);
            return contacts;
        }
        
       
        

    }
}
