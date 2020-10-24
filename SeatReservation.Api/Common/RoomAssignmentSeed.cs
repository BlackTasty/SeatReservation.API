using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class RoomAssignmentSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<RoomAssignment> roomAssignments)
        {
            if (roomAssignments?.Count > 0 && databaseContext.RoomAssignments.Count() <= 0)
            {
                foreach (var roomAssignment in roomAssignments.ToList())
                {
                    databaseContext.RoomAssignments.Add(roomAssignment);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
