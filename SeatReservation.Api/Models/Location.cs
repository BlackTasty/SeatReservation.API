using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Adresse
        [Required]
        public string Address { get; set; }

        // PLZ
        [Required]
        public int ZipCode { get; set; }

        // Land
        [Required]
        public string Country { get; set; }

        // Bundesland
        [Required]
        public string State { get; set; }

        public string Logo { get; set; }

        public int LogoImageId { get; set; }

        [Required]
        public bool IsShutdown { get; set; }
    }
}
