using Microsoft.AspNetCore.Hosting;
using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class FileRepository : IFileRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly IHostingEnvironment host;

        public FileRepository(DatabaseContext databaseContext, IHostingEnvironment host)
        {
            this.databaseContext = databaseContext;
            this.host = host;
        }

        public int Upload(MediaFile file)
        {
            try
            {
                Log.Information("Trying to upload file...");
                ICollection<MediaFile> files = databaseContext.Files.ToList();
                int id = files.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = files.Any(s => s.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                file.Id = id;

                databaseContext.Files.Add(file);
                databaseContext.SaveChanges();

                Log.Information("File uploaded and saved.");
                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred while uploading file!");
                return -1;
            }
        }

        public MediaFile GetById(int id)
        {
            Log.Information("Returning file with id " + id + "...");
            return databaseContext.Files.FirstOrDefault(p => p.Id == id);
        }

        public bool RemoveFile(int id)
        {
            try
            {
                Log.Information("Trying to remove file with id " + id + "...");
                MediaFile file = GetById(id);
                if (file == null)
                {
                    Log.Warning("No file with this id found!");
                    return false;
                }

                databaseContext.Files.Remove(file);
                databaseContext.SaveChanges();

                string filePath = Path.Combine(host.WebRootPath, "uploads", file.FileType == MediaFileType.Image ? "image" : "video", file.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                Log.Information("File removed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred while removing file!");
                return false;
            }
        }
    }
}
