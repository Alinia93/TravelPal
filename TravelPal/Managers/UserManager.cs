using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public static class UserManager
    {
        public static List<IUser> users = new()
        { new Admin("admin", "password", Country.Sweden),
            new User("user", "password", Country.Syria) {travels=new List<Travel> { new Travel("Madrid", Country.Spain, 2, new System.DateTime(2023, 01, 05), new System.DateTime(2023, 01, 12), 5), new Travel("Paris",Country.France,1, new System.DateTime(2019,04,01),new System.DateTime(2019,04,07),7) } }};
        public static IUser SignedInUser
        {
            get; set;
        }

        public static bool AddUser(IUser user)
        {
            if (ValidateUserName(user.UserName))
            {
                User newUser = new(user.UserName, user.Password, user.Location);
            }
            return false;
        }

        public static void RemoveUser(IUser iUser)
        {

        }

        public static bool UpdateUserName(IUser iUser, string hej)
        {
            return false;
        }

        private static bool ValidateUserName(string userName)
        {
            bool isUserNameValid = true;

            foreach (IUser user in users)
            {
                if (userName == user.UserName)
                {
                    isUserNameValid = false;
                }
                else
                {
                    isUserNameValid = true;
                }
            }

            return isUserNameValid;
        }

        public static bool SignInUser(string userName, string password)
        {

            bool isSignInValid = true;

            foreach (IUser user in users)
            {
                if (userName == user.UserName && password == user.Password)
                {
                    SignedInUser = user;
                    isSignInValid = true;
                }
                else
                {
                    isSignInValid = false;
                }
            }
            return isSignInValid;
        }
    }
}
