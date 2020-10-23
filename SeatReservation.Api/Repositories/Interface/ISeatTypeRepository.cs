using SeatReservation.Api.Models;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface ISeatTypeRepository
    {
        ICollection<SeatType> GetSeatTypes();

        Result AddSeatType(SeatType seatType);

        Result UpdateSeatType(SeatType seatType);

        Result RemoveSeatType(int seatTypeId);

        SeatType GetSeatTypeById(int seatTypeId);
    }
}
