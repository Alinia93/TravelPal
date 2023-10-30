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
        public TravelDetailsWindow(Travel travel)
        {
            InitializeComponent();

            TravelManager.DetailsTravel(travel);

            txtBCity.Text = travel.Destination;
            txtBCountry.Text = travel.Country.ToString();
            txtBStartDate.Text = travel.StartDate.ToShortDateString();
            txtBEndDate.Text = travel.EndDate.ToShortDateString();
            txtBNumberOfTravelers.Text = travel.Travelers.ToString();
            txtBTravelDays.Text = $" Days: {travel.TravelDays.ToString()}";


            foreach (PackingListItem packingListItem in travel.PackingList)
            {

                ListBoxItem item = new();
                item.Content = packingListItem.GetInfo();
                lstPackingList.Items.Add(item);
            }

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
            if (ValidateCountry())
            {
                TravelManager.CurrentTravel.Destination = txtBCity.Text;

                if (ValidateNumber(txtBNumberOfTravelers.Text, out int numberOftravelers))
                {
                    TravelManager.CurrentTravel.Travelers = numberOftravelers;
                    if (ValidateStartTimeAndEndTime(txtBStartDate.Text, out DateTime startDate))
                    {
                        TravelManager.CurrentTravel.StartDate = startDate;
                        if (ValidateStartTimeAndEndTime(txtBEndDate.Text, out DateTime endDate))
                        {
                            TravelManager.CurrentTravel.EndDate = endDate;
                            CheckAllInclusive();
                            CheckMeetingDetails();
                        }
                        else
                        {
                            MessageBox.Show("The format on \"End Date\" is wrong. Write in the format: yyyy-mm-dd.", "Warning");
                        }

                    }
                    else
                    {
                        MessageBox.Show("The format on \"Start Date\" is wrong Write in the format: yyyy-mm-dd", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Number of travelers has to be a number, Try again.");
                }

            }
            else
            {
                MessageBox.Show("That country does not seem to exist. Are you sure you spelled it right?");
            }
        }

        public void GoToMainWindow()
        {
            MessageBox.Show("Your edits are saved.");
            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }

        public void CheckMeetingDetails()
        {
            if (TravelManager.CurrentTravel.GetType() == typeof(WorkTrip))
            {
                WorkTrip workTrip = (WorkTrip)TravelManager.CurrentTravel;

                workTrip.MeetingDetails = txtBMeetingDetailsOrAllInclusive.Text;

                GoToMainWindow();

            }
        }

        public void CheckAllInclusive()
        {
            if (TravelManager.CurrentTravel.GetType() == typeof(Vacation))
            {
                Vacation vacation = (Vacation)TravelManager.CurrentTravel;

                if (txtBMeetingDetailsOrAllInclusive.Text == "yes" || txtBMeetingDetailsOrAllInclusive.Text == "Yes")
                {
                    vacation.AllInclusive = true;
                    GoToMainWindow();

                }
                else if (txtBMeetingDetailsOrAllInclusive.Text == "no" || txtBMeetingDetailsOrAllInclusive.Text == "No")
                {
                    vacation.AllInclusive = false;
                    GoToMainWindow();

                }
                else
                {
                    MessageBox.Show("You have to write \"Yes\" or \"No\" on the All inclusive Window");
                }

            }

        }

        // Metod för att valuera om start/end time är skrivet i rätt format
        public bool ValidateStartTimeAndEndTime(string number, out DateTime result)
        {
            bool IsStartOrEndTimeValid;
            return IsStartOrEndTimeValid = DateTime.TryParse(number, out result);
        }

        //Metod för att validera om det nya skrivna "country" finns i enum Country
        public bool ValidateCountry()
        {

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                if (country.ToString() == txtBCountry.Text)
                {
                    Country newCountry = (Country)country;
                    TravelManager.CurrentTravel.Country = newCountry;
                    return true;
                }


            }
            return false;

        }
        //Metod för att valuera om användaren skrivit en siffra på number of travelers
        public bool ValidateNumber(string number, out int result)
        {
            bool isNumbervalid;
            return isNumbervalid = int.TryParse(number, out result);
        }
    }
}
