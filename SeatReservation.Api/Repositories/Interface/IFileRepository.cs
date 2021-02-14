using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface IFileRepository
    {
        int Upload(MediaFile photo);

        bool RemoveFile(int id);

        MediaFile GetById(int id);
    }
}
