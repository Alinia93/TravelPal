using System;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        public AddTravelWindow()
        {
            InitializeComponent();

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                cmbBCountry.Items.Add(country);
            }


            cmbBWorkTripOrVaccation.Items.Add("Vacation");
            cmbBWorkTripOrVaccation.Items.Add("Work trip");


        }

        private void cmbBWorkTripOrVaccation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = (string)cmbBWorkTripOrVaccation.SelectedItem;

            if (selectedItem == "Vacation")
            {
                txtAllInclusiveOrMeetingDetails.Content = "All inclusive:";
                checkBAllInclusive.Visibility = Visibility.Visible;
                txtBMeetingDetails.Visibility = Visibility.Hidden;
            }
            else if (selectedItem == "Work trip")
            {
                txtAllInclusiveOrMeetingDetails.Content = "Meeting details:";
                txtBMeetingDetails.Visibility = Visibility.Visible;
                checkBAllInclusive.Visibility = Visibility.Hidden;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string city = txtBCity.Text;
            string startDate = txtBStartDate.Text;
            string endDate = txtBEndDate.Text;
            int numberOfPassenger = Convert.ToInt32(txtBNumberOfPassenger.Text);

            if (city != "" && startDate != "" && endDate != "" && numberOfPassenger! > 0 && cmbBCountry.SelectedIndex != -1 && cmbBWorkTripOrVaccation.SelectedIndex != -1)
            {
                User user = (User)UserManager.SignedInUser;
                Country country = (Country)cmbBCountry.SelectedItem;
                DateTime newStartDate = DateTime.Parse(startDate);
                DateTime newEndDate = DateTime.Parse(endDate);


                string selectedItem = (string)cmbBWorkTripOrVaccation.SelectedItem;

                if (selectedItem == "Vacation")
                {
                    if (checkBAllInclusive.IsChecked == true)
                    {

                        Vacation newVacation = new(city, country, numberOfPassenger, newStartDate, newEndDate, 0, true);
                        user.travels.Add(newVacation);


                    }
                    else if (checkBAllInclusive.IsChecked == false)
                    {
                        Vacation newVacation = new(city, country, numberOfPassenger, newStartDate, newEndDate, 0, false);
                        user.travels.Add(newVacation);

                    }


                }
                else if (selectedItem == "Work trip")
                {
                    string meetingDetails = txtBMeetingDetails.Text;
                    WorkTrip newWorkTrip = new(city, country, numberOfPassenger, newStartDate, newEndDate, 0, meetingDetails);
                    user.travels.Add(newWorkTrip);
                }


                TravelsWindow travelsWindow = new();
                travelsWindow.Show();
                Close();


            }
        }
    }
}

