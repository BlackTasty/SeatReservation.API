using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class ScheduleCopyTargetDto
    {
        public ICollection<int> ScheduleSlotIds { get; set; }

        public ICollection<int> RoomIds { get; set; }

        public DateTime TargetDate { get; set; }
    }
}
