using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class RoomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ScheduleId { get; set; }

        public ScheduleDto Schedule { get; set; }

        public int RoomPlanId { get; set; }

        public RoomPlanDto RoomPlan { get; set; }

        public bool IsOpen { get; set; }
    }
}
