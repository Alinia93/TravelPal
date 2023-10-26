using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public static class TravelManager
    {
        public static Travel CurrentTravel { get; set; }

        public static void DetailsTravel(Travel travel)
        {
            CurrentTravel = travel;
        }
        public static List<Travel> GetAllTravels()
        {
            foreach (IUser iUser in UserManager.users)
            {
                if (iUser.GetType() == typeof(User))
                {
                    List<Travel> allTravels = new();
                    User newUser = (User)iUser;

                    foreach (Travel travel in newUser.travels)
                    {
                        allTravels.Add(travel);
                    }
                    return allTravels;
                }

            }
            return null;
        }

        public static void AddTravel(Travel travel)
        {

        }

        public static void RemoveTravel(Travel travel)
        {

            if (UserManager.SignedInUser.GetType() == typeof(User))
            {
                User user = (User)UserManager.SignedInUser;

                for (int i = 0; i < user.travels.Count; i++)
                {
                    if (user.travels[i] == travel)
                    {
                        user.travels.RemoveAt(i);
                    }
                }

            }
        }
    }
}
