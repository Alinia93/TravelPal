using System;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                ComboBoxItem item = new();
                item.Content = country;
                item.Tag = country;
                cmbBCountry.Items.Add(item);
            }
        }
    }
}
