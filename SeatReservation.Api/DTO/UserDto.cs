using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegisterDate { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int PostalCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public ICollection<PermissionDto> Permissions { get; set; }
    }
}
