using SeatReservation.Api.Configuration;
using SeatReservation.Api.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class FileSeed
    {
        public static void Seed(DatabaseContext portalContext, FileConfiguration photoConfiguration)
        {
            if (photoConfiguration != null && portalContext.Files.Count() <= 0)
            {
                foreach (var photo in photoConfiguration.Photos.ToList())
                {
                    portalContext.Files.Add(photo);
                }

                portalContext.SaveChanges();
            }
        }
    }
}
