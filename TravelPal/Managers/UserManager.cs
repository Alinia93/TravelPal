using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public static class UserManager
    {

        //Lista med alla IUser 
        public static List<IUser> users = new()
        {
            new Admin("admin","password",  Country.Sweden),
            new User("user", "password", Country.Sweden)
            {
                travels = new List<Travel>
                {
                    new Vacation("Madrid",
                        Country.Spain,
                        2,
                        new System.DateTime(2023, 01, 05),
                        new System.DateTime(2023, 01, 12),
                        new List<PackingListItem>
                        {
                            new OtherItem("Shampoo",2)
                        },
                        true),
                    new WorkTrip("Paris",
                        Country.France,
                        1,
                        new System.DateTime(2019,04,01),
                        new System.DateTime(2019,04,07),
                        new List<PackingListItem>
                        {
                            new TravelDocument("Passport",true)
                        },
                        "Meeting a client from Berlin at Las Ramblas 54")
            } }
        };


        public static IUser? SignedInUser
        {
            get; private set;
        }

        /*Metod som tar emot en IUser. Skickar user name till ValidateUserName. 
         om ValidateUserName är true. Skapar den en User och lägger till i users listan
         */
        public static bool AddUser(IUser user)
        {
            bool hasAddedUser = false;
            if (ValidateUserName(user.UserName))
            {
                User newUser = new(user.UserName, user.Password, user.Location);
                users.Add(newUser);
                hasAddedUser = true;
            }

            return hasAddedUser;
        }

        public static bool UpdateUserName(IUser user)
        {
            if (ValidateUserName(user.UserName))
            {
                SignedInUser.UserName = user.UserName;
                SignedInUser.Password = user.Password;
                SignedInUser.Location = user.Location;
                return true;
            }
            return false;

        }

        private static bool ValidateUserName(string userName)
        {
            foreach (IUser user in users)
            {
                if (user.UserName == SignedInUser.UserName)
                {
                    continue;
                }
                if (userName == user.UserName)
                {
                    return false;
                }
            }

            return true;
        }

        /*
         Metod som kollar om det finns en IUser i users listan som har
        samma användarnamn/lösenord. Om det finns det sätts SignedInUser till den IUsern
        */
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

        public static void SignOutUser()
        {
            SignedInUser = null;
        }
    }
}
