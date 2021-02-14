using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class LocationDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Adresse
        public string Address { get; set; }

        // PLZ
        public int ZipCode { get; set; }

        // Land
        public string Country { get; set; }

        // Bundesland
        public string State { get; set; }

        public MediaFileDto Logo { get; set; }

        public bool IsShutdown { get; set; }

        public ICollection<RoomDto> Rooms { get; set; }
    }
}
