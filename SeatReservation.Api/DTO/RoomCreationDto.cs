using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class RoomCreationDto
    {
        public RoomDto Room { get; set; }

        public int LocationId { get; set; }
    }
}
