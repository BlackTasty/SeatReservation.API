using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DatabaseContext databaseContext;

        public ReservationRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Result AddReservation(Reservation reservation)
        {
            try
            {
                databaseContext.Reservations.Add(reservation);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result CancelReservation(int reservationId)
        {
            try
            {
                Reservation reservation = databaseContext.Reservations.FirstOrDefault(x => x.Id == reservationId);

                if (reservation == null)
                {
                    return new Result(false);
                }

                //Check if reservation is older than 30 minutes or if movie start is less than 15 minutes away. If so deny the cancellation
                if (DateTime.Now.Subtract(reservation.BookingDate).TotalMinutes <= 30 || reservation.ScheduleSlot.Start.Subtract(DateTime.Now).TotalMinutes <= 15)
                {
                    return new Result(false);
                }

                databaseContext.Reservations.Remove(reservation);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Reservation GetById(int id)
        {
            return databaseContext.Reservations.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Reservation> GetReservations()
        {
            return databaseContext.Reservations.ToList();
        }
    }
}
