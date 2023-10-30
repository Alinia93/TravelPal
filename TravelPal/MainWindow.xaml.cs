﻿using System.Windows;
using TravelPal.Managers;

namespace TravelPal
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
            Close();


        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            string userName = txtBUserName.Text;
            string password = txtBPassword.Password;

            // Skickar användarnamn och lösen till metod i UserManager. 

            bool isUserSignedIn = UserManager.SignInUser(userName, password);

            if (isUserSignedIn)
            {
                TravelsWindow travelsWindow = new();
                travelsWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("User name or password is not correct!", "Warning");
                txtBPassword.Password = "";
                txtBUserName.Text = "";
            }
        }
    }
}
