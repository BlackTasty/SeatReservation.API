using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class ScheduleDto
    {
        public int Id { get; set; }

        public ICollection<ScheduleSlotDto> MovieSchedule { get; set; }
    }
}
