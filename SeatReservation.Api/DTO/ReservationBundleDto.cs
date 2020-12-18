using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class ReservationBundleDto
    {
        public string ReservationNumber { get; set; }

        public ICollection<ReservationDto> Reservations { get; set; }

        public MovieDto Movie { get; set; }

        public ReservationDataDto ReservationData { get; set; }

        public ScheduleSlotDto ScheduleSlot { get; set; }

        public bool CanCancel { get; set; }
    }
}
