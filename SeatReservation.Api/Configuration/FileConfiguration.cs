using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class FileConfiguration
    {
        public ICollection<MediaFile> Photos { get; set; }
    }
}
