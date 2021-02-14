using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Interface;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment host;
        private readonly IFileRepository fileRepository;
        private readonly IMapper mapper;
        private readonly IParser parser;

        public FileService(IHostingEnvironment host, IFileRepository fileRepository, IMapper mapper, IParser parser)
        {
            this.host = host;
            this.fileRepository = fileRepository;
            this.mapper = mapper;
            this.parser = parser;
        }

        public int UploadFile(IFormFile fileData, MediaFileType fileType)
        {
            string uploadsDirectory = Path.Combine(host.WebRootPath, "uploads", fileType == MediaFileType.Image ? "image" : "video");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileData.FileName);
            string filePath = Path.Combine(uploadsDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                fileData.CopyTo(stream);
            }

            MediaFile mediaFile = new MediaFile()
            {
                FileName = fileName,
                DataType = FileDataType.File,
                FileType = fileType
            };

            return fileRepository.Upload(mediaFile);
        }

        public MediaFileDto UploadVideo(UploadDto fileUpload)
        {
            MediaFile mediaFile = SaveFile(fileUpload, MediaFileType.Video);
            fileRepository.Upload(mediaFile);
            return parser.ToMediaFileDto(mediaFile);
        }

        public MediaFileDto UploadImage(UploadDto fileUpload)
        {
            MediaFile mediaFile = SaveFile(fileUpload, MediaFileType.Image);
            fileRepository.Upload(mediaFile);
            return parser.ToMediaFileDto(mediaFile);
        }

        private MediaFile SaveFile(UploadDto fileUpload, MediaFileType fileType)
        {
            string uploadsDirectory = Path.Combine(host.WebRootPath, "uploads", fileType == MediaFileType.Image ? "image" : "video");
            if (fileUpload.FileCategory != FileCategory.None)
            {
                uploadsDirectory = Path.Combine(uploadsDirectory, GetCategoryFolder(fileUpload.FileCategory));
            }

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }
            //string fileName = (!string.IsNullOrWhiteSpace(fileUpload.CustomFileName) ? fileUpload.CustomFileName : Guid.NewGuid().ToString()) + Path.GetExtension(fileUpload.FileData.FileName);
            string fileName = null;
            string filePath = Path.Combine(uploadsDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                fileUpload.FileData.CopyTo(stream);
            }

            return new MediaFile()
            {
                FileName = fileName,
                FileType = fileType
            };
        }

        public MediaFileDto GetById(int id)
        {
            MediaFile file = fileRepository.GetById(id);
            if (file == null)
            {
                return null;
            }

            string filePath = Path.Combine(host.WebRootPath, "uploads", file.FileType == MediaFileType.Image ? "image" : "video");
            filePath = file.FileCategory != FileCategory.None ? Path.Combine(filePath, GetCategoryFolder(file.FileCategory), file.FileName) : Path.Combine(filePath, file.FileName);
            MediaFileDto fileDto = new MediaFileDto()
            {
                Id = file.Id,
                FilePath = filePath,
                DataType = file.DataType
            };

            return fileDto;
        }

        public bool RemoveFile(int id)
        {
            return fileRepository.RemoveFile(id);
        }

        private string GetCategoryFolder(FileCategory fileCategory)
        {
            switch (fileCategory)
            {
                case FileCategory.Location:
                    return "location";
                case FileCategory.Movie:
                    return "movie";
                case FileCategory.Seat:
                    return "seat";
                case FileCategory.User:
                    return "user";
                default:
                    return "";
            }
        }

        /*private string ImageToBase64(string fileName)
        {
            string filePath = Path.Combine(host.WebRootPath, "uploads", fileName);
            byte[] fileRaw = File.ReadAllBytes(filePath);
            return "data:image/png;base64," + Convert.ToBase64String(fileRaw);
        }

        private string VideoToBase64(string fileName)
        {
            string filePath = Path.Combine(host.WebRootPath, "uploads", fileName);
            byte[] fileRaw = File.ReadAllBytes(filePath);
            return "data:video/png;base64," + Convert.ToBase64String(fileRaw);
        }*/
    }
}
