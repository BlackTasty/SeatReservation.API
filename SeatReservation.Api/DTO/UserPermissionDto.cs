using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class UserPermissionDto
    {
        public int UserId { get; set; }

        public ICollection<PermissionDto> Permissions { get; set; }
    }
}
