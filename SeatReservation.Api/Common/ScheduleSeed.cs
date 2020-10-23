using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class ScheduleSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Schedule> schedules)
        {
            if (schedules?.Count > 0 && databaseContext.Schedules.Count() <= 0)
            {
                foreach (var schedule in schedules.ToList())
                {
                    databaseContext.Schedules.Add(schedule);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
