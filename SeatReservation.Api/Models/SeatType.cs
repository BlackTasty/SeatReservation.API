using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class SeatType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string SeatImage { get; set; }

        [Required]
        public int SeatCount { get; set; } = 1;
    }
}
