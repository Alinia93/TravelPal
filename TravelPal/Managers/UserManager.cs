using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public static class UserManager
    {
        public static List<IUser> users = new()
        { new Admin("admin","password",  Country.Sweden),
            new User("user", "password", Country.Sweden) {travels=new List<Travel> { new Vacation("Madrid", Country.Spain, 2, new System.DateTime(2023, 01, 05), new System.DateTime(2023, 01, 12),0,true), new WorkTrip("Paris",Country.France,1, new System.DateTime(2019,04,01),new System.DateTime(2019,04,07),0, "Meeting a client from Berlin at Las Ramblas 54") } }};
        public static IUser SignedInUser
        {
            get; set;
        }

        public static bool AddUser(IUser user)
        {

            bool IsAddUser = true;
            if (ValidateUserName(user.UserName))
            {
                User newUser = new(user.UserName, user.Password, user.Location);
                IsAddUser = true;
                users.Add(newUser);
            }
            else
            {
                IsAddUser = false;
            }
            return IsAddUser;

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


            foreach (IUser iUser in users)
            {
                if (userName == iUser.UserName && password == iUser.Password)
                {
                    SignedInUser = iUser;
                    return true;
                }


            }
            return false;
        }
    }
}
