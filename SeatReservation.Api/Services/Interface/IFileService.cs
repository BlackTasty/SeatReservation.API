using Microsoft.AspNetCore.Http;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface IFileService
    {
        int UploadFile(IFormFile fileData, MediaFileType fileType);

        MediaFileDto UploadVideo(UploadDto fileUpload);

        MediaFileDto UploadImage(UploadDto fileUpload);

        //bool Update(PhotoDto photo);

        bool RemoveFile(int id);

        MediaFileDto GetById(int id);
    }
}
