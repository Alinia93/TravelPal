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
        public int TravelsDays { get; set; }
        public Travel()
        {

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
