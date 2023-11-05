using System;
using System.Windows;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();

            txtBNewUserName.Text = UserManager.SignedInUser.UserName;

            Country signedInUserCountry = UserManager.SignedInUser.Location;

            cmbBChangeCountry.Items.Add(signedInUserCountry);
            cmbBChangeCountry.SelectedItem = signedInUserCountry;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {



            if (txtBNewUserName.Text.Length <= 3)
            {
                MessageBox.Show("Your user name has to be longer than 3 signs", "Warning");
                return;
            }


            if (txtBNewPassword.Text.Length <= 5 && txtBNewPassword.Text.Length > 0)
            {
                MessageBox.Show("Your password must be longer than 5 signs");
                return;
            }
            if (txtBNewPassword.Text != txtBComfirmPassword.Text)
            {
                MessageBox.Show(" \"New password\" and \"Comfirm password\" does not match");
                return;
            }

            //Om användaren har skrivit något i newPassword
            if (!string.IsNullOrEmpty(txtBNewPassword.Text))
            {
                //uppdatera användare med nytt password
                User user = new(txtBNewUserName.Text, txtBComfirmPassword.Text, (Country)cmbBChangeCountry.SelectedItem);
                if (UserManager.UpdateUserName(user))
                {
                    MessageBox.Show("Your changes has been saved", "Saved");
                    GoBack();
                }
                else
                {
                    MessageBox.Show("The user name is occupied. Try Again");
                }

            }
            // om användaren inte skrivit något i newPassword
            if (string.IsNullOrEmpty(txtBNewPassword.Text))
            {
                // Uppdatera användaren med sitt gamla lösenord
                User user = new(txtBNewUserName.Text, UserManager.SignedInUser!.Password, (Country)cmbBChangeCountry.SelectedItem);
                if (UserManager.UpdateUserName(user))
                {
                    MessageBox.Show("Your changes has been saved!", "Saved");
                    GoBack();
                }
                else
                {
                    MessageBox.Show("The user name is occupied. Try Again");
                    return;
                }








            }

        }


        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void btnChangeUserName_Click(object sender, RoutedEventArgs e)
        {
            txtUserName.Content = "New user name:";
            txtBNewUserName.Text = "";
            txtBNewUserName.IsReadOnly = false;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            txtNewPassword.Visibility = Visibility.Visible;
            txtComfirmNewPassword.Visibility = Visibility.Visible;
            txtBNewPassword.Visibility = Visibility.Visible;
            txtBComfirmPassword.Visibility = Visibility.Visible;
        }

        private void btnChangeCoutnry_Click(object sender, RoutedEventArgs e)
        {
            txtChangeCountry.Visibility = Visibility.Visible;
            cmbBChangeCountry.IsReadOnly = false;

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                cmbBChangeCountry.Items.Add(country);
            }
        }
        public void GoBack()
        {
            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }
    }
}

