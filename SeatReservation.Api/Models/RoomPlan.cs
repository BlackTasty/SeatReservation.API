using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class RoomPlan
    {
        public int Id { get; set; }

        public string Seats { get; set; }

        [Required]
        public int Columns { get; set; } = 1;

        [Required]
        public int Rows { get; set; } = 1;
    }
}
