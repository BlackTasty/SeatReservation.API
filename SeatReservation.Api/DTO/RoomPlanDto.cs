using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class RoomPlanDto
    {
        public int Id { get; set; }

        public ICollection<SeatPositionDto> Seats { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }
    }
}
