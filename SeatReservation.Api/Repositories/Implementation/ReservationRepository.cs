using SeatReservation.Api.Database;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly IScheduleRepository scheduleRepository;

        public ReservationRepository(DatabaseContext databaseContext, IScheduleRepository scheduleRepository)
        {
            this.databaseContext = databaseContext;
            this.scheduleRepository = scheduleRepository;
        }

        public Result AddReservation(ICollection<Reservation> reservations)
        {
            try
            {
                if (reservations.Count == 0)
                {
                    return new Result(false);
                }

                ICollection<Reservation> dbReservations = GetReservations().ToList();
                foreach (Reservation reservation in reservations)
                {
                    if (dbReservations.Any(x => x.ScheduleSlotId == reservation.ScheduleSlotId && x.SeatId == reservation.SeatId))
                    {
                        return new Result(false);
                    }
                }

                int id = dbReservations.Count + 1;
                bool duplicateId = false;
                foreach (Reservation reservation in reservations)
                {
                    do
                    {
                        duplicateId = dbReservations.Any(x => x.Id == id);
                        if (duplicateId)
                        {
                            id++;
                        }
                    } while (duplicateId);
                    reservation.Id = id;
                    id++;
                }

                if (!scheduleRepository.AddReservationsToScheduleSlot(reservations.ElementAt(0).ScheduleSlotId, reservations).Success)
                {
                    return new Result(false);
                }

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

        public ICollection<Reservation> GetReservationsForUserId(int userId, bool getReservationHistory)
        {
            List<Reservation> reservationHistory = new List<Reservation>();
            DateTime now = DateTime.Now;
            foreach (Reservation reservation in databaseContext.Reservations.Where(x => x.UserId == userId).ToList())
            {
                ScheduleSlot slot = scheduleRepository.GetScheduleSlotById(reservation.ScheduleSlotId);
                if (slot.Start < now && getReservationHistory || slot.Start >= now && !getReservationHistory)
                {
                    reservationHistory.Add(reservation);
                }
            }
            return reservationHistory;
        }
    }
}
