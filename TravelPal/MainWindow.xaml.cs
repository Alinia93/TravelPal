using System.Windows;
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
            UpdateIsSignUpButtonEnabled();
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

            // Skickar användarnamn och lösenord till metod - SignInUser i UserManager 

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

        private void txtBUserName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateIsSignUpButtonEnabled();
        }

        private void txtBPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateIsSignUpButtonEnabled();
        }
        //Metod för att sign up button inte ska enabled innan man fyllt i password/user name
        private void UpdateIsSignUpButtonEnabled()
        {
            bool hasPassword = !string.IsNullOrWhiteSpace(txtBPassword.Password);
            bool hasUserName = !string.IsNullOrWhiteSpace(txtBUserName.Text);
            btnSignIn.IsEnabled = hasPassword && hasUserName;
        }
    }
}
