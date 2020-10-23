using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class SeatPosition
    {
        public int Id { get; set; }

        public int SeatTypeId { get; set; }

        [Required]
        public int Column { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Rotation { get; set; }
    }
}
