using SeatReservation.Api.DTO;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface ISeatTypeService
    {
        ICollection<SeatTypeDto> GetSeatTypes();

        Result AddSeatType(SeatTypeDto seatTypeDto);

        Result UpdateSeatType(SeatTypeDto seatTypeDto);

        Result RemoveSeatType(int seatTypeId);
    }
}
