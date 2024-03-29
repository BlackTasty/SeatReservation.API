﻿using SeatReservation.Api.Models;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetReservations();

        Reservation GetById(int id);

        Result AddReservation(ICollection<Reservation> reservations);

        Result CancelReservation(Reservation reservation, int userId);

        ICollection<Reservation> GetReservationsForSchedule(int scheduleId);

        ICollection<Reservation> GetReservationsForUserId(int userId, bool getReservationHistory);
    }
}
