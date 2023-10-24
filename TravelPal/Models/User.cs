﻿using System.Collections.Generic;

namespace TravelPal.Models
{
    class User : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }

        public List<Travel> travels { get; set; }

    }
}
