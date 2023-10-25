using System;

namespace TravelPal.Models
{
    public class WorkTrip : Travel
    {
        public string MeetingDetails { get; set; }


        public WorkTrip(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, int travelDays, string meetingDetails) : base(destination, country, travelers, startDate, endDate, travelDays)
        {
            MeetingDetails = meetingDetails;
        }

        public override string GetInfo()
        {
            return null;
        }
    }
}
