using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Services.Interface;
using SeatReservation.Api.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;

        private readonly string[] ACCEPTED_IMAGE_TYPES = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] ACCEPTED_VIDEO_TYPES = new[] { ".mp4", ".ogg" };

        private static readonly int MAX_VIDEO_REAL_SIZE = 100; // In megabytes. Declares max file size
        private readonly long MAX_VIDEO_SIZE = MAX_VIDEO_REAL_SIZE * 1024 * 1024; // In bytes, approx. 100 MB. Declared by MAX_VIDEO_REAL_SIZE

        private static readonly int MAX_IMAGE_REAL_SIZE = 10; // In megabytes. Declares max file size
        private readonly long MAX_IMAGE_SIZE = MAX_IMAGE_REAL_SIZE * 1024 * 1024; // In bytes, approx. 10 MB. Declared by MAX_IMAGE_REAL_SIZE

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet("checkurl"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CheckUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                // return HTTP error 400; Indicates that the provided url is either null or empty (including whitespaces)
                return BadRequest("Keine URL hochgeladen!");
            }

            if (!url.StartsWith("https://www.youtube.com/watch?v="))
            {
                long remoteFileSize = GetFileSize(url);
                if (remoteFileSize > MAX_IMAGE_SIZE)
                {
                    double uploadedFileSize = Math.Round((double)remoteFileSize / 1024 / 1024);
                    double maxFileSize = Math.Round((double)MAX_IMAGE_SIZE / 1024 / 1024);
                    return BadRequest(string.Format("URL Inhalt um {0} MB zu groß! (max. {1} MB, hochzuladenes Bild: {2} MB)", uploadedFileSize - MAX_IMAGE_REAL_SIZE, maxFileSize, uploadedFileSize));
                }

                Uri uri = new Uri(url);
                string fileName = Path.GetFileName(uri.AbsolutePath);

                string extension = Path.GetExtension(url).ToLower();
                extension = extension.Contains('?') ? extension.Substring(0, extension.IndexOf('?')) : extension;
                if (!ACCEPTED_IMAGE_TYPES.Any(s => s == extension))
                {
                    return BadRequest(string.Format("Dateiformat wird nicht unterstützt! (unterstützt: {0})", string.Join(", ", ACCEPTED_IMAGE_TYPES)));
                }
            }

            return Ok(true);
        }

        [HttpPost("uploadvideo"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UploadVideo([FromBody]UploadDto fileUpload)
        {
            Result result = ProcessVideo(fileUpload);
            if (result.Success)
            {
                return Ok(fileService.UploadVideo(fileUpload));
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("uploadimage"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UploadImage([FromForm]IFormFile fileData)
        {
            if (fileData == null)
            {
                return BadRequest("Kein Bild hochgeladen!");
            }

            if (fileData.Length > MAX_IMAGE_SIZE)
            {
                double uploadedFileSize = Math.Round((double)fileData.Length / 1024 / 1024);
                double maxFileSize = Math.Round((double)MAX_IMAGE_SIZE / 1024 / 1024);
                return BadRequest(string.Format("Bild um {0} MB zu groß! (max. {1} MB, hochzuladenes Bild: {2} MB)", uploadedFileSize - MAX_IMAGE_REAL_SIZE, maxFileSize, uploadedFileSize));
            }

            if (!ACCEPTED_IMAGE_TYPES.Any(s => s == Path.GetExtension(fileData.FileName).ToLower()))
            {
                return BadRequest(string.Format("Dateiformat wird nicht unterstützt! (unterstützt: {0})", string.Join(", ", ACCEPTED_IMAGE_TYPES)));
            }
            return Ok(fileService.UploadFile(fileData, MediaFileType.Image));

            /*Result result = ProcessImage(fileUpload);
            if (result.Success)
            {
                return Ok(result.Id);
            }
            else
            {
                return BadRequest(result.Message);
            }*/
        }

        [HttpPost("uploadurl"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public IActionResult UploadUrl(UploadDto urlUpload)
        {
            if (urlUpload.DataType == FileDataType.Url)
            {
                // return HTTP error 409; Indicates that the provided data type is not an URL
                return Conflict("Falscher Datentyp verwendet! (Erlaubter Datentyp: URLs)");
            }

            string url = urlUpload.Url;

            if (string.IsNullOrWhiteSpace(url))
            {
                // return HTTP error 400; Indicates that the provided url is either null or empty (including whitespaces)
                return BadRequest(string.Format("Kein {0} hochgeladen!", urlUpload.FileType == MediaFileType.Image ? "image" : "video"));
            }

            if (urlUpload.DataType != FileDataType.YouTube)
            {
                long remoteFileSize = GetFileSize(url);
                if (remoteFileSize > MAX_IMAGE_SIZE)
                {
                    double uploadedFileSize = Math.Round((double)remoteFileSize / 1024 / 1024);
                    double maxFileSize = Math.Round((double)MAX_IMAGE_SIZE / 1024 / 1024);
                    return BadRequest(string.Format("Bild um {0} MB zu groß! (max. {1} MB, hochzuladenes Bild: {2} MB)", uploadedFileSize - MAX_IMAGE_REAL_SIZE, maxFileSize, uploadedFileSize));
                }

                Uri uri = new Uri(url);
                string fileName = Path.GetFileName(uri.AbsolutePath);

                if (!ACCEPTED_IMAGE_TYPES.Any(s => s == Path.GetExtension(url).ToLower()))
                {
                    return BadRequest(string.Format("Dateiformat wird nicht unterstützt! (unterstützt: {0})", string.Join(", ", ACCEPTED_IMAGE_TYPES)));
                }
            }

            return Ok(urlUpload.FileType == MediaFileType.Image ? fileService.UploadImage(urlUpload) : fileService.UploadVideo(urlUpload));

            /*Result result = ProcessImage(fileUpload);
            if (result.Success)
            {
                return Ok(result.Id);
            }
            else
            {
                return BadRequest(result.Message);
            }*/
        }

        private long GetFileSize(string url)
        {
            long result = -1;

            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            req.Method = "HEAD";
            using (System.Net.WebResponse resp = req.GetResponse())
            {
                if (long.TryParse(resp.Headers.Get("Content-Length"), out long ContentLength))
                {
                    result = ContentLength;
                }
            }

            return result;
        }

        [HttpPost("uploadmultiple"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UploadMultiple([FromBody]ICollection<UploadDto> fileUploads)
        {
            List<FileUploadResultDto> results = new List<FileUploadResultDto>();
            foreach (UploadDto fileUpload in fileUploads)
            {
                if (fileUpload.IsEmpty)
                {
                    results.Add(new FileUploadResultDto()
                    {
                        FileEmpty = true
                    });
                    continue;
                }

                Result result;
                if (fileUpload.FileType == MediaFileType.Image)
                {
                    result = ProcessImage(fileUpload);
                }
                else
                {
                    result = ProcessVideo(fileUpload);
                }

                results.Add(new FileUploadResultDto()
                {
                    FileId = result.Id,
                    ErrorMessage = result.Message
                });
                if (result.Id <= 0)
                {
                    Log.Error("Couldn't process file! Error message: " + result.Message);
                }
            }

            return Ok(results);
        }

        //[HttpPost("update")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(409)]
        //public IActionResult Update([FromBody]PhotoDto photo)
        //{
        //    if (photoService.Update(photo))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return Conflict();
        //    }
        //}

        [HttpDelete("removefile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public IActionResult RemoveFile(int id)
        {
            if (fileService.RemoveFile(id))
            {
                return Ok(true);
            }
            else
            {
                return Conflict(false);
            }
        }

        [HttpGet("getbyid")]
        [ProducesResponseType(200)]
        public IActionResult GetById(int id)
        {
            return Ok(fileService.GetById(id));
        }

        private Result ProcessImage(UploadDto fileUpload)
        {
            //IFormFile fileData = fileUpload.FileData;
            IFormFile fileData = null;
            if (fileData == null)
            {
                return new Result("Kein Bild hochgeladen!");
            }

            if (fileData.Length > MAX_IMAGE_SIZE)
            {
                double uploadedFileSize = Math.Round((double)fileData.Length / 1024 / 1024);
                double maxFileSize = Math.Round((double)MAX_IMAGE_SIZE / 1024 / 1024);
                return new Result(string.Format("Bild um {0} MB zu groß! (max. {1} MB, hochzuladenes Bild: {2} MB)", uploadedFileSize - MAX_IMAGE_REAL_SIZE, maxFileSize, uploadedFileSize));
            }

            if (!ACCEPTED_IMAGE_TYPES.Any(s => s == Path.GetExtension(fileData.FileName).ToLower()))
            {
                return new Result(string.Format("Dateiformat wird nicht unterstützt! (unterstützt: {0})", string.Join(", ", ACCEPTED_IMAGE_TYPES)));
            }

            return new Result(fileService.UploadImage(fileUpload).Id);
        }

        private Result ProcessVideo(UploadDto fileUpload)
        {
            //IFormFile fileData = fileUpload.FileData;
            IFormFile fileData = null;
            if (fileData == null)
            {
                return new Result("Kein Video hochgeladen!");
            }

            if (fileData.Length > MAX_VIDEO_SIZE)
            {
                double uploadedFileSize = Math.Round((double)fileData.Length / 1024 / 1024);
                double maxFileSize = Math.Round((double)MAX_VIDEO_SIZE / 1024 / 1024);
                return new Result(string.Format("Video um {0} MB zu groß! (max. {1} MB, hochzuladenes Video: {2} MB)", uploadedFileSize - MAX_VIDEO_REAL_SIZE, maxFileSize, uploadedFileSize));
            }

            if (!ACCEPTED_VIDEO_TYPES.Any(s => s == Path.GetExtension(fileData.FileName).ToLower()))
            {
                return new Result(string.Format("Dateiformat wird nicht unterstützt! (unterstützt: {0})", string.Join(", ", ACCEPTED_VIDEO_TYPES)));
            }

            return new Result(fileService.UploadVideo(fileUpload).Id);
        }
    }
}
