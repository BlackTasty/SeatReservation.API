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

        Result AddReservation(ICollection<ReservationDto> reservations);

        Result CancelReservation(int reservationId, int userId);

        ICollection<ReservationDto> GetReservationsForSchedule(int scheduleId);

        ICollection<ReservationBundleDto> GetReservationsForUserId(int userId, bool getReservationHistory);

        ReservationDataDto GetReservationData(ICollection<ReservationDto> reservations);

        bool CanCancelReservation(int reservationId);
    }
}
