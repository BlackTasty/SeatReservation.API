using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public enum ReservationStatus
    {
        Free, // Green
        Reserved, // Yellow
        Sold, // Red
        Unavailable, // Grey
        Invisible
    }
}
