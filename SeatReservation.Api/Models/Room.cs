using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ScheduleId { get; set; }

        public int RoomPlanId { get; set; }

        [Required]
        public bool IsOpen { get; set; } = true;
    }
}
