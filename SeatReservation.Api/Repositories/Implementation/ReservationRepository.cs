using SeatReservation.Api.Database;
using SeatReservation.Api.DTO;
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

        public Result AddReservation(ICollection<Reservation> reservations)
        {
            try
            {
                foreach (Reservation reservation in reservations)
                {
                    databaseContext.Reservations.Add(reservation);
                }
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result CancelReservation(Reservation reservation, int userId)
        {
            try
            {
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

        public ICollection<Reservation> GetReservationsForSchedule(int scheduleId)
        {
            return databaseContext.Reservations.Where(x => x.ScheduleSlotId == scheduleId).ToList();
        }
    }
}
