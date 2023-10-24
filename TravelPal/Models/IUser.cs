namespace TravelPal.Models
{
    public interface IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }
    }
}
