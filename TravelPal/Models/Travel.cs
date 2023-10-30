using System;
using System.Collections.Generic;

namespace TravelPal.Models
{
    public class Travel
    {

        int x;
        public string Destination { get; set; }
        public Country Country { get; set; }
        public int Travelers { get; set; }
        public List<PackingListItem> PackingList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { set { x = CalculateTravelDays(); } get { return x; } }
        public Travel(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, int travelDays, List<PackingListItem> packingListItem)
        {
            Destination = destination;
            Country = country;
            Travelers = travelers;

            StartDate = startDate;
            EndDate = endDate;
            TravelDays = travelDays;
            PackingList = packingListItem;

        }

        public virtual string GetInfo()
        {
            return null;
        }

        private int CalculateTravelDays()
        {
            int number = Convert.ToInt32((EndDate - StartDate).TotalDays);
            return number;
        }
    }
}
