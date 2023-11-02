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

            // Om användaren inte tryckt på "Change password" 
            if (txtBNewPassword.Visibility == Visibility.Hidden)
            {
                if (txtBNewUserName.Text.Length <= 3)
                {
                    MessageBox.Show("Your user name has to be longer than 3 signs", "Warning");
                    return;
                }
                // Skapa ny User och skicka till UserManager UpdateUserName
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
            else if (txtBNewPassword.Visibility == Visibility.Visible)
            {
                if (txtBNewPassword.Text.Length <= 5)
                {
                    MessageBox.Show("Your password must be longer than 5 signs");
                    return;
                }
                if (txtBNewPassword.Text != txtBComfirmPassword.Text)
                {
                    MessageBox.Show(" \"New password\" and \"Comfirm password\" does not match");
                    return;
                }

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

