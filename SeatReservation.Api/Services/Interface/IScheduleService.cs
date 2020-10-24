using SeatReservation.Api.Util;
using SeatReservation.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface IScheduleService
    {
        ICollection<ScheduleDto> GetSchedules();

        ScheduleSlotDto GetScheduleSlotById(int slotId);

        Result AddSchedule(ScheduleDto schedule);

        Result RemoveSchedule(int scheduleId);

        Result ScheduleMovie(int roomId, ScheduleSlotDto scheduleSlot);

        Result RemoveScheduledMovie(int roomId, int scheduleSlotId);

        int GenerateSchedule(bool writeToDatabase);
    }
}
