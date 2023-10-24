namespace TravelPal.Models
{
    public class WorkTrip : Travel
    {
        public string MeetingDetails { get; set; }


        public WorkTrip(string meetingDetails)
        {
            MeetingDetails = meetingDetails;
        }

        public override string GetInfo()
        {
            return null;
        }
    }
}
