using SeatReservation.Api.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class RoomAssignment
    {
        public int Id { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public string RoomIds { get; set; }
    }
}
