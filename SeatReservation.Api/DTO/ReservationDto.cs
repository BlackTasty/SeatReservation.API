using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public int SeatId { get; set; }

        public SeatTypeDto Seat { get; set; }

        public int ScheduleSlotId { get; set; }

        public ScheduleSlotDto ScheduleSlot { get; set; }

        public int RoomId { get; set; }

        public RoomDto Room { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
