namespace TravelPal.Models
{
    public class TravelDocument : PackingListItem
    {

        public bool Required { get; set; }
        public string Name { get; set; }

        public TravelDocument(string name, bool required)
        {
            Name = name;
            required = Required;
        }

        public string GetInfo()
        {
            return null;
        }
    }
}
