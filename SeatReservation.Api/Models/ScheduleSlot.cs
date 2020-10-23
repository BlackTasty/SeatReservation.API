using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class ScheduleSlot
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int ScheduleId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public string Reservations { get; set; }
    }
}
