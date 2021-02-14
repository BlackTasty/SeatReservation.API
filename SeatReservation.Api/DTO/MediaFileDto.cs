using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class MediaFileDto
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public FileDataType DataType { get; set; }
    }
}
