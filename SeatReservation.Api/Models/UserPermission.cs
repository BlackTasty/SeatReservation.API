using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class UserPermission
    {
        public int UserId { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
