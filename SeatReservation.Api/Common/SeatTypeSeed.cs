using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class SeatTypeSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<SeatType> seatTypes)
        {
            if (seatTypes?.Count > 0 && databaseContext.SeatTypes.Count() <= 0)
            {
                foreach (var seatType in seatTypes.ToList())
                {
                    databaseContext.SeatTypes.Add(seatType);
                }

                databaseContext.SaveChanges();
            }
        }

        internal static void Seed(DatabaseContext context, object seatTypes)
        {
            throw new NotImplementedException();
        }
    }
}
