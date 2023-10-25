using System;
using System.Collections.Generic;

namespace TravelPal.Models
{
    public class Travel
    {
        public string Destination { get; set; }
        public Country Countries { get; set; }
        public int Travelers { get; set; }
        public List<PackingListItem> PackingList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }
        public Travel(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, int travelDays)
        {
            Destination = destination;
            Countries = country;
            Travelers = travelers;

            StartDate = startDate;
            EndDate = endDate;
            TravelDays = travelDays;

        }

        public virtual string GetInfo()
        {
            return null;
        }

        private int CalculateTravelDays()
        {
            return 0;
        }
    }
}
