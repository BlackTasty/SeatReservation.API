using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class SeatTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SeatImage { get; set; }

        public int SeatCount { get; set; }

        public double BasePrice { get; set; }

        public ICollection<SeatPositionDto> SeatPosition { get; set; }
    }
}
