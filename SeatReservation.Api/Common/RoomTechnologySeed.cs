using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class RoomTechnologySeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<RoomTechnology> technologies)
        {
            if (technologies?.Count > 0 && databaseContext.RoomTechnologies.Count() <= 0)
            {
                foreach (var technology in technologies.ToList())
                {
                    databaseContext.RoomTechnologies.Add(technology);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
