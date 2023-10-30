using System;
using System.Collections.Generic;

namespace TravelPal.Models
{
    public class WorkTrip : Travel
    {
        public string MeetingDetails { get; set; }


        public WorkTrip(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, int travelDays, List<PackingListItem> packingListItem, string meetingDetails) : base(destination, country, travelers, startDate, endDate, travelDays, packingListItem)
        {
            MeetingDetails = meetingDetails;

        }

        public override string GetInfo()
        {
            return null;
        }
    }
}
