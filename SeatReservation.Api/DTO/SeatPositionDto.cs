using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class SeatPositionDto
    {
        public int Id { get; set; }

        public int SeatTypeId { get; set; }

        public SeatTypeDto SeatType { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        public int Rotation { get; set; }
    }
}
