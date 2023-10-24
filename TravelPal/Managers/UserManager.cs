using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    class UserManager
    {
        public static List<IUser> users = new() { new Admin}
        public IUser SignedInUser { get; set; }


        public bool AddUser(IUser iUser)
        {
            return false;
        }

        public void RemoveUser(IUser iUser)
        {

        }

        public bool UpdateUserName(IUser iUser, string hej)
        {
            return false;
        }

        private bool ValidateUserName(string userName)
        {
            return false;
        }

        public bool SignInUser(string userName, string password)
        {
            return false;
        }
    }
}
