using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class RoomPlanSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<RoomPlan> roomPlans)
        {
            if (roomPlans?.Count > 0 && databaseContext.RoomPlans.Count() <= 0)
            {
                foreach (var roomPlan in roomPlans.ToList())
                {
                    databaseContext.RoomPlans.Add(roomPlan);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
