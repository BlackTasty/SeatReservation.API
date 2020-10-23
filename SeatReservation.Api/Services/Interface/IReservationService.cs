using SeatReservation.Api.DTO;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface IReservationService
    {
        ICollection<ReservationDto> GetReservations();

        ReservationDto GetById(int id);

        Result AddReservation(ReservationDto reservation);

        Result CancelReservation(int reservationId);
    }
}
