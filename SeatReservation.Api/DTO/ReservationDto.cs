using Newtonsoft.Json;
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

        public int ScheduleSlotId { get; set; }

        public int RoomId { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public DateTime BookingDate { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public bool IsConfirmed { get; set; }
        
        public string ReservationNumber { get; set; }
    }
}
