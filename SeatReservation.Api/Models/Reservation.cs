using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int SeatId { get; set; }

        public int ScheduleSlotId { get; set; }

        public int RoomId { get; set; }

        [Required]
        public ReservationStatus ReservationStatus { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Currently unused
        /// </summary>
        public bool IsConfirmed { get; set; }
    }
}
