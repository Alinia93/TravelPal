using System;
using System.Collections.Generic;
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
            string numberOfPassenger = txtBNumberOfPassenger.Text;

            if (city != "" && startDate != "" && endDate != "" && numberOfPassenger != "" && cmbBCountry.SelectedIndex != -1 && cmbBWorkTripOrVaccation.SelectedIndex != -1)
            {
                User user = (User)UserManager.SignedInUser;
                Country country = (Country)cmbBCountry.SelectedItem;
                if (TravelManager.ValidateStartDateAndEndDate(startDate, out DateTime startDateresult))
                {
                    DateTime newStartDate = startDateresult;
                    if (TravelManager.ValidateStartDateAndEndDate(endDate, out DateTime endDateresult))
                    {
                        DateTime newEndDate = endDateresult;
                        if (TravelManager.ValidateNumber(numberOfPassenger, out int result))
                        {
                            int convertedNumberOfPassenger = result;
                            List<PackingListItem> packingListItems = AddPackingList();
                            string selectedItem = (string)cmbBWorkTripOrVaccation.SelectedItem;

                            if (selectedItem == "Vacation")
                            {
                                bool isAllInclusive = checkBAllInclusive.IsChecked == true;

                                Vacation newVacation = new(city, country, convertedNumberOfPassenger, newStartDate, newEndDate, packingListItems, isAllInclusive);
                                user.travels.Add(newVacation);

                                ;
                            }
                            else if (selectedItem == "Work trip")
                            {
                                string meetingDetails = txtBMeetingDetails.Text;
                                WorkTrip newWorkTrip = new(city, country, convertedNumberOfPassenger, newStartDate, newEndDate, packingListItems, meetingDetails);
                                user.travels.Add(newWorkTrip);

                            }

                            TravelsWindow travelsWindow = new();
                            travelsWindow.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Number of passenger is in wrong format!", "Warning");
                        }
                    }
                    else
                    {
                        MessageBox.Show("End Date wrong format.");
                    }

                }
                else
                {
                    MessageBox.Show("Start Date in wrong format");
                }

            }
            else
            {
                MessageBox.Show("You have not filled in all the required information", "Warning");

            }



        }


        public List<PackingListItem> AddPackingList()
        {

            User user = (User)UserManager.SignedInUser;
            if (user.travels == null)
            {
                user.travels = new List<Travel>();
            }

            List<PackingListItem> packingListItems = new();

            foreach (ListBoxItem listBoxItem in lstPackingList.Items)

            {


                if (listBoxItem.Tag is TravelDocument travelDocument)
                {
                    packingListItems.Add(travelDocument);
                }
                else if (listBoxItem.Tag is OtherItem otherItem)
                {
                    packingListItems.Add(otherItem);
                }



            }
            return packingListItems;


        }

        private void checkBTravelDocuemnt_Checked(object sender, RoutedEventArgs e)
        {
            checkBRequired.Visibility = Visibility.Visible;
            txtRequired.Visibility = Visibility.Visible;
            txtBQuantity.Visibility = Visibility.Hidden;
            txtQuantity.Visibility = Visibility.Hidden;

        }

        private void checkBTravelDocuemnt_Unchecked(object sender, RoutedEventArgs e)
        {
            txtRequired.Visibility = Visibility.Hidden;
            checkBRequired.Visibility = Visibility.Hidden;
            txtBQuantity.Visibility = Visibility.Visible;
            txtQuantity.Visibility = Visibility.Visible;
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (txtBItemToPack.Text != "")
            {
                string itemToPack = txtBItemToPack.Text;
                int quantity;

                if (string.IsNullOrEmpty(txtBQuantity.Text))
                {
                    quantity = 1;
                }
                else
                {
                    quantity = Convert.ToInt32(txtBQuantity.Text);
                }


                if (checkBTravelDocuemnt.IsChecked == false)
                {

                    OtherItem newItem = new(itemToPack, quantity);
                    ListBoxItem item = new();
                    item.Content = $"Item: {newItem.Name}. Quantity: {newItem.Quantity} ";
                    item.Tag = newItem;
                    lstPackingList.Items.Add(item);

                }
                else if (checkBTravelDocuemnt.IsChecked == true)
                {
                    bool isRequired = checkBRequired.IsChecked == true;


                    TravelDocument newTravelDocument = new(itemToPack, isRequired);
                    ListBoxItem item = new();
                    item.Content = $"Item: {newTravelDocument.Name}. Required: {newTravelDocument.GetInfo()}";
                    item.Tag = newTravelDocument;
                    lstPackingList.Items.Add(item);



                }




            }

        }


        public bool PassportRequired()
        {
            bool isPassPortRequired = false;
            bool isSignedInUserLocationEuCountry = false;
            bool isSelectedItemEuCountry = false;
            string? selectedItem = cmbBCountry.SelectedItem.ToString();
            string userLocation = UserManager.SignedInUser.Location.ToString();


            foreach (var euCountry in Enum.GetValues(typeof(EuropeanCountry)))
            {
                if (userLocation == euCountry.ToString())
                {
                    isSignedInUserLocationEuCountry = true;
                    break;
                }

            }

            foreach (var euCountry in Enum.GetValues(typeof(EuropeanCountry)))
            {
                if (selectedItem == euCountry.ToString())
                {
                    isSelectedItemEuCountry = true;
                    break;
                }

            }

            if (isSignedInUserLocationEuCountry && !isSelectedItemEuCountry)
            {
                isPassPortRequired = true;

            }
            else if (!isSignedInUserLocationEuCountry)
            {
                isPassPortRequired = true;
            }


            return isPassPortRequired;

        }



        private void cmbBCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstPackingList.Items.Clear();
            bool isPassportRequired = PassportRequired();

            if (isPassportRequired)
            {
                TravelDocument newTravelDocument = new("Passport", true);
                ListBoxItem item = new();
                item.Content = $"Passport. Required:Yes";
                item.Tag = newTravelDocument;
                lstPackingList.Items.Add(item);
            }
            else
            {
                TravelDocument newTravelDocument = new("Passport", false);
                ListBoxItem item = new();
                item.Content = $"Passport. Required:No";
                item.Tag = newTravelDocument;
                lstPackingList.Items.Add(item);
            }
        }
    }
}

