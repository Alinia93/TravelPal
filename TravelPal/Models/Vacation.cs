using System;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {

        public bool AllInclusive { get; set; }

        public Vacation(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, int travelDays, bool allInclusive) : base(destination, country, travelers, startDate, endDate, travelDays)
        {
            AllInclusive = allInclusive;
        }

        public override string GetInfo()
        {
            return null;
        }
    }
}
