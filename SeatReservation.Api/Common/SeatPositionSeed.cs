using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class SeatPositionSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<SeatPosition> seatPositions)
        {
            if (seatPositions?.Count > 0 && databaseContext.SeatPositions.Count() <= 0)
            {
                foreach (var seatPosition in seatPositions.ToList())
                {
                    databaseContext.SeatPositions.Add(seatPosition);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
