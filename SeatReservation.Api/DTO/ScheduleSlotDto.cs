using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class ScheduleSlotDto
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int ScheduleId { get; set; }

        public MovieDto Movie { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ICollection<ReservationDto> Reservations { get; set; }
    }
}
