using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int PostalCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
        
        public string Email { get; set; }

        [Required]
        public string Permissions { get; set; }

        public int AvatarImageId { get; set; }
    }
}
