using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public static class TravelManager
    {
        public static Travel CurrentTravel { get; set; }
        public static List<Travel> allTravels;

        public static List<Travel> GetAllTravels()
        {
            List<Travel> travelsList = new();
            for (int i = 0; i < UserManager.users.Count; i++)
            {
                if (UserManager.users[i].GetType() == typeof(User))
                {
                    User newUser = (User)UserManager.users[i];
                    for (int j = 0; j < newUser.travels.Count; j++)
                    {
                        travelsList.Add(newUser.travels[j]);
                    }
                }
            }
            return travelsList;

        }
        public static void DetailsTravel(Travel travel)
        {
            CurrentTravel = travel;
        }



        public static void AddTravel(Travel travel)
        {

        }

        //Metod för att ta bort en travel från en user. 
        public static void RemoveTravel(Travel travel)
        {
            //Om inloggad användare är en user
            if (UserManager.SignedInUser.GetType() == typeof(User))
            {

                User user = (User)UserManager.SignedInUser;

                //Loopa igenom travels på inloggad användare
                for (int i = 0; i < user.travels.Count; i++)
                {
                    if (user.travels[i] == travel)
                    {

                        user.travels.RemoveAt(i);
                    }
                }

            }
            //Om inloggad användare är en Admin
            else if (UserManager.SignedInUser.GetType() == typeof(Admin))
            {
                //Loopa igenom alla IUsers
                for (int i = 0; i < UserManager.users.Count; i++)
                {
                    if (UserManager.users[i].GetType() == typeof(User))
                    {
                        User newUser = (User)UserManager.users[i];
                        //Loopa igenom travels på usern 
                        for (int j = 0; j < newUser.travels.Count; j++)
                        {
                            if (travel == newUser.travels[j])
                            {
                                newUser.travels.RemoveAt(j);
                            }
                        }
                    }
                }
            }
        }
    }
}
