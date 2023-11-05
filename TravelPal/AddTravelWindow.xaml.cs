using System;
using System.Collections.Generic;
using System.Linq;
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
            //Metod för att ändra UI om användaren väljer Vacation eller Work Trip 
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

            // Validera användarens input 
            if (city == "" || startDate == "" || endDate == "" || numberOfPassenger == "" || cmbBCountry.SelectedIndex == -1 || cmbBWorkTripOrVaccation.SelectedIndex == -1)
            {
                MessageBox.Show("You have not filled in all the required information", "Warning");
                return;

            }
            if (!TravelManager.ValidateStartDateAndEndDate(endDate, out DateTime endDateresult))
            {
                MessageBox.Show("End Date is in the wrong format. The correct format should be YYYY-MM-DD", "Warning");
                return;
            }
            if (!TravelManager.ValidateStartDateAndEndDate(startDate, out DateTime startDateresult))
            {
                MessageBox.Show("Start Date in the wrong format. The correct format should be YY-MM-DD", "Warning");
                return;
            }
            // Kolla så End Date inte är större än Start Date
            if (endDateresult < startDateresult)
            {
                MessageBox.Show("End Date can not be earlier than Start Date");
                return;
            }
            if (!TravelManager.ValidateNumber(numberOfPassenger, out int result))
            {
                MessageBox.Show("Number of passenger is in the wrong format!", "Warning");
                return;
            }



            // Spara alla värden 
            Country country = (Country)cmbBCountry.SelectedItem;
            DateTime newStartDate = startDateresult;

            DateTime newEndDate = endDateresult;


            int convertedNumberOfPassenger = result;
            List<PackingListItem> packingListItems = AddPackingList();
            string selectedItem = (string)cmbBWorkTripOrVaccation.SelectedItem;

            // Om användaren valt Vacation 
            if (selectedItem == "Vacation")
            {
                bool isAllInclusive = checkBAllInclusive.IsChecked == true;

                // Skapa en ny Vacation 
                Vacation newVacation = new(city, country, convertedNumberOfPassenger, newStartDate, newEndDate, packingListItems, isAllInclusive);
                //Skicka Vacation till TravelManager AddTravel
                TravelManager.AddTravel(newVacation);

            }
            // Om användaren valt Work Trip 
            else if (selectedItem == "Work trip")
            {
                //Skapa ny Work Trip 
                string meetingDetails = txtBMeetingDetails.Text;
                WorkTrip newWorkTrip = new(city, country, convertedNumberOfPassenger, newStartDate, newEndDate, packingListItems, meetingDetails);
                //Skicka Work Trip till Travel Manager AddTravel 
                TravelManager.AddTravel(newWorkTrip);

            }

            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }


        public List<PackingListItem> AddPackingList()
        {
            // Metod för att skapa en lista med packingListItems
            User user = (User)UserManager.SignedInUser!;


            List<PackingListItem> packingListItems = new();
            //Loopa igenom Listboxen 
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
                //Metod för att skapa PackingListItem och lägga i listbox
                string itemToPack = txtBItemToPack.Text;
                int quantity;

                // Om användaren inte väljer quantity så blir det automatiskt 1 
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
                    item.Content = newItem.GetInfo();
                    item.Tag = newItem;
                    lstPackingList.Items.Add(item);
                    txtBItemToPack.Text = "";
                    txtBQuantity.Text = "";

                }
                else if (checkBTravelDocuemnt.IsChecked == true)
                {
                    bool isRequired = checkBRequired.IsChecked == true;


                    TravelDocument newTravelDocument = new(itemToPack, isRequired);
                    ListBoxItem item = new();
                    item.Content = newTravelDocument.GetInfo();
                    item.Tag = newTravelDocument;
                    lstPackingList.Items.Add(item);
                    txtBItemToPack.Text = "";
                    checkBTravelDocuemnt.IsChecked = false;
                    checkBRequired.IsChecked = false;
                }


            }

        }


        public bool PassportRequired()
        {
            //Metod för att kolla om passport är reqired 
            bool isPassPortRequired = false;

            string? selectedItem = cmbBCountry.SelectedItem.ToString();
            string userLocation = UserManager.SignedInUser.Location.ToString();
            // Omvandla Enum med Countries till en lista med strings
            List<string> euCountries = Enum.GetNames(typeof(EuropeanCountry)).ToList();
            //Om userns location finns i listan med strings men inte selectedItem gör
            if (euCountries.Contains(userLocation) && !(euCountries.Contains(selectedItem)))
            {
                isPassPortRequired = true;
            }
            if (!(euCountries.Contains(userLocation)))
            {
                isPassPortRequired = true;
            }
            return isPassPortRequired;


        }




        private void cmbBCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Metod för att automatiskt lägga in pass med antingen required true/false när användaren väljer land
            lstPackingList.Items.Clear();
            bool isPassportRequired = PassportRequired();

            if (isPassportRequired)
            {

                TravelDocument newTravelDocument = new("Passport", true);
                ListBoxItem item = new();
                item.Content = newTravelDocument.GetInfo();
                item.Tag = newTravelDocument;
                lstPackingList.Items.Add(item);
            }
            else
            {
                TravelDocument newTravelDocument = new("Passport", false);
                ListBoxItem item = new();
                item.Content = newTravelDocument.GetInfo();

                item.Tag = newTravelDocument;
                lstPackingList.Items.Add(item);
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new();
            travelsWindow.Show();
            Close();
        }
    }
}

