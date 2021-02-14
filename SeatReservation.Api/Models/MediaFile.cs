using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class MediaFile
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public MediaFileType FileType { get; set; }

        public FileDataType DataType { get; set; }

        public FileCategory FileCategory { get; set; }
    }
}
