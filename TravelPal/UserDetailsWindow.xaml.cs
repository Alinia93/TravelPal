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

            txtUserName.Content = UserManager.SignedInUser.UserName;

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                cmbBChangeCountry.Items.Add(country);
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtBNewUserName.Text != "")
            {
                bool isUserNameValid = ValidateUserName(txtBNewUserName.Text);
                if (isUserNameValid)
                {
                    UserManager.SignedInUser.UserName = txtBNewUserName.Text;
                    MessageBox.Show("Saved!");
                    TravelsWindow travelsWindow = new();
                    travelsWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("User name is occupied. Try again");
                }
            }

            if (txtBNewPassword.Text != "")
            {
                if (txtBNewPassword.Text == txtBComfirmPassword.Text)
                {
                    UserManager.SignedInUser.Password = txtBComfirmPassword.Text;
                    MessageBox.Show("Saved!");
                    TravelsWindow travelsWindow = new();
                    travelsWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("You have not written the same password in \"New password\" and \"Comfirm password\"");
                }
            }

            if (cmbBChangeCountry.SelectedIndex != -1)
            {
                Country country = (Country)cmbBChangeCountry.SelectedItem;
                UserManager.SignedInUser.Location = country;
                MessageBox.Show("Saved!");
                TravelsWindow travelsWindow = new();
                travelsWindow.Show();
                Close();
            }




        }


        public bool ValidateUserName(string userName)
        {
            foreach (IUser iUser in UserManager.users)
            {

                if (userName == UserManager.SignedInUser.UserName)
                {
                    continue;
                }

                if (userName == iUser.UserName)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }
    }
}

