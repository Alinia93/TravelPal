namespace TravelPal.Models
{
    class Admin : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }
    }
}
