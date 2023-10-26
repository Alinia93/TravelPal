namespace TravelPal.Models
{
    public class Admin : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }

        public Admin(string userName, string password, Country location)
        {
            UserName = userName;
            Password = password;
            Location = location;
        }
    }
}
