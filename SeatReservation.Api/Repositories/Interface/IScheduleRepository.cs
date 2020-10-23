﻿using SeatReservation.Api.Util;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface IScheduleRepository
    {
        ICollection<Schedule> GetSchedules();

        Result AddSchedule(Schedule schedule);

        Result RemoveSchedule(int scheduleId);

        Result ScheduleMovie(int roomId, ScheduleSlot scheduleSlot);

        Result RemoveScheduledMovie(int roomId, int scheduleSlotId);

        Schedule GetScheduleById(int scheduleId);

        ScheduleSlot GetScheduleSlotById(int scheduleSlotId);

        int GenerateSchedule(bool writeToDatabase);
    }
}
