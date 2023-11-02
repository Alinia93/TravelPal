using System;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        Travel _travel;
        public TravelDetailsWindow(Travel travel)
        {
            InitializeComponent();



            _travel = travel;
            txtBCity.Text = _travel.Destination;
            txtBCountry.Text = _travel.Country.ToString();
            txtBStartDate.Text = _travel.StartDate.ToShortDateString();
            txtBEndDate.Text = _travel.EndDate.ToShortDateString();
            txtBNumberOfTravelers.Text = _travel.Travelers.ToString();
            txtBTravelDays.Text = $" Days: {_travel.TravelDays.ToString()}";


            foreach (PackingListItem packingListItem in _travel.PackingList)
            {

                ListBoxItem item = new();
                item.Content = packingListItem.GetInfo();
                lstPackingList.Items.Add(item);
            }

            if (_travel.GetType() == typeof(Vacation))

            {
                Vacation vacationTravel = (Vacation)_travel;
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
            else if (_travel.GetType() == typeof(WorkTrip))
            {
                WorkTrip workTrip = (WorkTrip)_travel;
                txtBTypeOfTravel.Text = "Work Trip";
                txtAllInclusiveOrMeetingDetails.Content = "Meeting Details";
                txtBMeetingDetailsOrAllInclusive.Text = workTrip.MeetingDetails;
            }



        }

        private void btnGoToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            GoToMainWindow();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("You can now edit your trip. Then press save");
            txtBCountry.IsReadOnly = false;
            txtBCity.IsReadOnly = false;
            txtBStartDate.IsReadOnly = false;
            txtBEndDate.IsReadOnly = false;

            txtBNumberOfTravelers.IsReadOnly = false;
            txtBMeetingDetailsOrAllInclusive.IsReadOnly = false;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //Validera alla input rutor 
            if (!ValidateCountry())
            {
                MessageBox.Show("That country does not seem to exist. Are you sure you spelled it right?", "Warning");
                return;
            }

            if (!TravelManager.ValidateNumber(txtBNumberOfTravelers.Text, out int numberOftravelers))
            {
                MessageBox.Show("Number of travelers has to be a number, Try again.", "Warning");
                return;
            }
            if (!TravelManager.ValidateStartDateAndEndDate(txtBStartDate.Text, out DateTime startDate))
            {
                MessageBox.Show("The format on \"Start Date\" is wrong Write in the format: yyyy-mm-dd", "Warning");
                return;
            }
            if (!TravelManager.ValidateStartDateAndEndDate(txtBEndDate.Text, out DateTime endDate))
            {
                MessageBox.Show("The format on \"End Date\" is wrong. Write in the format: yyyy-mm-dd.", "Warning");
                return;
            }


            _travel.Destination = txtBCity.Text;
            _travel.Travelers = numberOftravelers;
            _travel.StartDate = startDate;
            _travel.EndDate = endDate;
            CheckAllInclusive();
            CheckMeetingDetails();


        }

        public void GoToMainWindow()
        {

            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }

        public void CheckMeetingDetails()
        {
            //Om travel är en Work Trip 
            if (_travel.GetType() == typeof(WorkTrip))
            {
                WorkTrip workTrip = (WorkTrip)_travel;

                //spara meetind details på travel
                workTrip.MeetingDetails = txtBMeetingDetailsOrAllInclusive.Text;

                MessageBox.Show("Your edits are saved", "Saved");
                GoToMainWindow();

            }
        }

        public void CheckAllInclusive()
        {
            //Om travel är en Vacation
            if (_travel.GetType() == typeof(Vacation))
            {
                Vacation vacation = (Vacation)_travel;

                // Kolla om input är "Yes or "No" 
                if (txtBMeetingDetailsOrAllInclusive.Text == "yes" || txtBMeetingDetailsOrAllInclusive.Text == "Yes")
                {
                    //Sätt all inclusive på true eller false
                    vacation.AllInclusive = true;
                    MessageBox.Show("Your edits are saved", "Saved");
                    GoToMainWindow();

                }
                else if (txtBMeetingDetailsOrAllInclusive.Text == "no" || txtBMeetingDetailsOrAllInclusive.Text == "No")
                {
                    vacation.AllInclusive = false;
                    MessageBox.Show("Your edits are saved", "Saved");
                    GoToMainWindow();

                }
                else
                {
                    MessageBox.Show("You have to write \"Yes\" or \"No\" on the All inclusive Window");
                }

            }

        }



        //Metod för att validera om det nya skrivna "country" finns i enum Country
        public bool ValidateCountry()
        {

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                if (country.ToString() == txtBCountry.Text)
                {
                    Country newCountry = (Country)country;
                    _travel.Country = newCountry;
                    return true;
                }


            }
            return false;

        }

    }
}
