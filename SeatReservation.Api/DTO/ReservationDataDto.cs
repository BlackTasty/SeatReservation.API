using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class ReservationDataDto
    {
        public ICollection<SeatPositionDto> SeatPositions { get; set; }

        public RoomTechnologyDto RoomTechnology { get; set; }

        public RoomDto Room { get; set; }
    }
}
