namespace TravelPal.Models
{
    class OtherItem : PackingListItem
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public OtherItem(int quantity)
        {
            Quantity = quantity;
        }

        public string GetInfo()
        {
            return null;
        }
    }
}
