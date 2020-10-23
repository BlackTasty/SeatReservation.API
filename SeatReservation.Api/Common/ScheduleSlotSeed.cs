using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class ScheduleSlotSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<ScheduleSlot> scheduleSlots)
        {
            if (scheduleSlots?.Count > 0 && databaseContext.ScheduleSlots.Count() <= 0)
            {
                foreach (var scheduleSlot in scheduleSlots.ToList())
                {
                    databaseContext.ScheduleSlots.Add(scheduleSlot);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
