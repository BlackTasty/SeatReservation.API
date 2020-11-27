using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Optional. Used when adding a new studio via movie creation/editing
        /// </summary>
        public int UniqueAddId { get; set; }
    }
}
