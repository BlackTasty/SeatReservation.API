using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class FileUploadResultDto
    {
        public int FileId { get; set; }

        public string ErrorMessage { get; set; }

        public bool FileEmpty { get; set; }
    }
}
