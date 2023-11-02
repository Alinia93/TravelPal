using System.Windows;
using System.Windows.Controls;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        public TravelsWindow()
        {
            InitializeComponent();

            txtBWelcomeUser.Text = $"Welcome {UserManager.SignedInUser.UserName}";


            if (UserManager.SignedInUser.GetType() == typeof(Admin))
            {
                txtYourTravels.Content = "All users travels:";
                btnAddTravel.IsEnabled = false;
            }

            UpdateUI();



        }

        private void btnInfoAboutTravel_Click(object sender, RoutedEventArgs e)
        {
            //Hämtar selected travel 
            Travel selectedTravel = GetSelectedTravel();
            if (selectedTravel != null)
            {
                //Skickar selected travel till travelDetails Window
                TravelDetailsWindow travelDetailsWindow = new(selectedTravel);
                travelDetailsWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("You must select a travel to see details!", "Warning");
            }
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new();
            addTravelWindow.Show();
            Close();
        }

        private void btnUserDetails_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new();
            userDetailsWindow.Show();
            Close();
        }

        //Metod som returnerar selected travel 
        private Travel GetSelectedTravel()
        {
            if (lstTravels.SelectedIndex != -1)
            {
                ListBoxItem item = (ListBoxItem)lstTravels.SelectedItem;

                return (Travel)item.Tag;
            }
            else
                return null;
        }
        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            Travel selectedTravel = GetSelectedTravel();
            if (selectedTravel != null)
            {
                //skickar selected travel till RemoveTravel i TravelManager
                TravelManager.RemoveTravel(selectedTravel);
                UpdateUI();
            }
            else
            {
                MessageBox.Show("You have to select a travel to remove it!", "Warning");
            }
        }


        public void UpdateUI()
        {
            //Rensa listan med travels
            lstTravels.Items.Clear();

            IUser signedInUser = UserManager.SignedInUser;

            // Om SignedInUser är en User 

            if (signedInUser.GetType() == typeof(User))
            {
                User user = (User)signedInUser;

                int number = 1;

                // Loopa igenom travels på SignedInUser och visa info
                foreach (Travel travel in user.travels)
                {
                    ListBoxItem item = new();
                    item.Content = $"{number}. {travel.GetInfo()}";
                    item.Tag = travel;

                    lstTravels.Items.Add(item);
                    number++;
                }

            }
            // Om SignedInUser är en Admin
            else if (signedInUser.GetType() == typeof(Admin))
            {
                //Anropa metod i TravelManager och få en lista med alla travels från Users
                foreach (Travel travel in TravelManager.GetAllTravels())
                {
                    ListBoxItem item = new();
                    item.Content = travel.GetInfo();
                    item.Tag = travel;
                    lstTravels.Items.Add(item);
                }

            }

        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            UserManager.SignOutUser();
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private void btnInfoAboutTravelPal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Travel Pal is the perfect application for people that travels a lot. Regardless if you travel within your work or if you are going on a relaxing vacation. Travel Pal helps you to plan your travel! ", "INFO ABOUT US");
        }
    }
}
