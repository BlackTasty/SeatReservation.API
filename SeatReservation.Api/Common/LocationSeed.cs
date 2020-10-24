using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class LocationSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Location> locations)
        {
            if (locations?.Count > 0 && databaseContext.Locations.Count() <= 0)
            {
                foreach (var location in locations.ToList())
                {
                    databaseContext.Locations.Add(location);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
