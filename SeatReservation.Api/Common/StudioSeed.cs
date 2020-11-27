using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class StudioSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Studio> studios)
        {
            if (studios?.Count > 0 && databaseContext.Studios.Count() <= 0)
            {
                foreach (var studio in studios.ToList())
                {
                    databaseContext.Studios.Add(studio);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
