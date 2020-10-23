using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class RoomSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Room> rooms)
        {
            if (rooms?.Count > 0 && databaseContext.Rooms.Count() <= 0)
            {
                foreach (var room in rooms.ToList())
                {
                    databaseContext.Rooms.Add(room);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
