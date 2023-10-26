using System;
using System.Windows;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        public TravelDetailsWindow(Travel travel)
        {
            InitializeComponent();

            TravelManager.DetailsTravel(travel);

            txtBCity.Text = travel.Destination;
            txtBCountry.Text = travel.Country.ToString();
            txtBDate.Text = $"{travel.StartDate.ToShortDateString()}-{travel.EndDate.ToShortDateString()}";
            txtBNumberOfTravelers.Text = travel.Travelers.ToString();
            txtBTravelDays.Text = $" Days: {travel.TravelDays.ToString()}";

            if (travel.GetType() == typeof(Vacation))

            {
                Vacation vacationTravel = (Vacation)travel;
                txtBTypeOfTravel.Text = "Vacation";
                txtAllInclusiveOrMeetingDetails.Content = "All Inclusive";

                if (vacationTravel.AllInclusive == true)
                {
                    txtBMeetingDetailsOrAllInclusive.Text = "Yes";
                }
                else
                {
                    txtBMeetingDetailsOrAllInclusive.Text = "No";
                }
            }
            else if (travel.GetType() == typeof(WorkTrip))
            {
                WorkTrip workTrip = (WorkTrip)travel;
                txtBTypeOfTravel.Text = "Work Trip";
                txtAllInclusiveOrMeetingDetails.Content = "Meeting Details";
                txtBMeetingDetailsOrAllInclusive.Text = workTrip.MeetingDetails;
            }



        }

        private void btnGoToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("You can now edit your trip. Then press save");
            txtBCountry.IsReadOnly = false;
            txtBCity.IsReadOnly = false;
            txtBDate.IsReadOnly = false;
            txtBTravelDays.IsReadOnly = false;
            txtBNumberOfTravelers.IsReadOnly = false;
            txtBMeetingDetailsOrAllInclusive.IsReadOnly = false;
            txtBTypeOfTravel.IsReadOnly = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            bool isCountryFound = false;
            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                if (country.ToString() == txtBCountry.Text)
                {
                    Country newCountry = (Country)country;
                    TravelManager.CurrentTravel.Country = newCountry;
                    isCountryFound = true;
                }


            }
            if (!isCountryFound)
            {
                MessageBox.Show("We can't find that country. Are you sure you spelled it right? Try again", "Warning");
            }


        }
    }
}
