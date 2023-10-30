using System;
using System.Windows;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            // Fyller combobox med länder från Enum - Country 
            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                cmbBCountry.Items.Add(country);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtBUserName.Text;
            string passWord = txtBPassword.Password;

            if (userName != "" && passWord != "" && cmbBCountry.SelectedIndex != -1)
            {
                if (userName.Length > 3)
                {
                    Country country = (Country)cmbBCountry.SelectedItem;

                    User newUser = new(userName, passWord, country);

                    if (UserManager.AddUser(newUser))
                    {
                        MessageBox.Show("Registration succeded!");
                        GoBackToMainWindow();
                    }
                    else
                    {
                        MessageBox.Show("The user name is occupied. Try again!", "Warning");
                        txtBUserName.Text = "";
                        txtBPassword.Password = "";

                    }
                }
                else
                {
                    MessageBox.Show("Your user name have to be more than 3 signs");
                }
            }
            else
            {
                MessageBox.Show("You have to fill in user name, password and your location!");


            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            GoBackToMainWindow();
        }

        //Metod som hoppar till Main Window och stänger Registration Window
        public void GoBackToMainWindow()
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
