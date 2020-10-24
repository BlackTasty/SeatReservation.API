using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class RoomTechnology
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double ExtraCharge { get; set; } = 0;

        public string Description { get; set; }
    }
}
