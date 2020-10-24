using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class RoomTechnologyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public double ExtraCharge { get; set; }

        public string Description { get; set; }
    }
}
