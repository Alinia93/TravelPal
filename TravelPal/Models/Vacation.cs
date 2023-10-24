namespace TravelPal.Models
{
    public class Vacation : Travel
    {

        public bool AllInclusive { get; set; }

        public Vacation(bool allInclusive)
        {
            AllInclusive = allInclusive;
        }

        public override string GetInfo()
        {
            return null;
        }
    }
}
