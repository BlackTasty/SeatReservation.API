using Microsoft.AspNetCore.Http;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class UploadDto
    {
        public string CustomFileName { get; set; }

        public dynamic FileData { get; set; }

        public string Url { get; set; }

        public MediaFileType FileType { get; set; }

        public FileDataType DataType { get; set; }

        public FileCategory FileCategory { get; set; }

        public bool IsEmpty { get; set; }
    }
}
