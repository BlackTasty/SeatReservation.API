using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public string MovieSchedule { get; set; }
    }
}
