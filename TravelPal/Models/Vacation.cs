﻿using System;
using System.Collections.Generic;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {

        public bool AllInclusive { get; set; }

        public Vacation(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, List<PackingListItem> packingListItem, bool allInclusive) : base(destination, country, travelers, startDate, endDate, packingListItem)
        {
            AllInclusive = allInclusive;
        }

        public override string GetInfo()
        {
            return $"Vacation - Destination: {Destination}. Country: {Country}";
        }
    }
}
