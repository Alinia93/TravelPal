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

            UpdateUI();



        }

        private void btnInfoAboutTravel_Click(object sender, RoutedEventArgs e)
        {
            if (lstTravels.SelectedIndex != -1)
            {
                ListBoxItem item = (ListBoxItem)lstTravels.SelectedItem;
                Travel travel = (Travel)item.Tag;

                TravelDetailsWindow travelDetailsWindow = new(travel);
                travelDetailsWindow.Show();
                Close();
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

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)

        {
            ListBoxItem item = (ListBoxItem)lstTravels.SelectedItem;

            Travel travel = (Travel)item.Tag;

            TravelManager.RemoveTravel(travel);
            UpdateUI();
        }


        public void UpdateUI()
        {
            lstTravels.Items.Clear();
            IUser signedInUser = UserManager.SignedInUser;

            if (signedInUser.GetType() == typeof(User))
            {
                User user = (User)signedInUser;

                if (user.travels != null)
                {
                    txtBWelcomeUser.Text = $"Welcome {user.UserName}";

                    int number = 1;

                    foreach (Travel travel in user.travels)
                    {
                        ListBoxItem item = new();
                        item.Content = $"{number}. Destination: {travel.Country}";
                        item.Tag = travel;

                        lstTravels.Items.Add(item);
                        number++;
                    }
                }
            }
            else if (signedInUser.GetType() == typeof(Admin))
            {
                foreach (Travel travel in TravelManager.GetAllTravels())
                {
                    ListBoxItem item = new();
                    item.Content = travel.Destination;
                    item.Tag = travel;
                    lstTravels.Items.Add(item);
                }

            }

        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
