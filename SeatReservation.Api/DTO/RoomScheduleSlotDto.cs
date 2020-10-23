using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class RoomScheduleSlotDto
    {
        public int RoomId { get; set; }

        public ScheduleSlotDto ScheduleSlot { get; set; }
    }
}
