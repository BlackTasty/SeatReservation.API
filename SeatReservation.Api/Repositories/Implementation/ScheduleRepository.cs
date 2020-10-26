using SeatReservation.Api.Util;
using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly IRoomRepository roomRepository;

        public ScheduleRepository(DatabaseContext databaseContext, IRoomRepository roomRepository)
        {
            this.databaseContext = databaseContext;
            this.roomRepository = roomRepository;
        }

        public Result AddReservationsToScheduleSlot(int scheduleSlotId, ICollection<Reservation> reservations)
        {
            try
            {
                ScheduleSlot scheduleSlot = GetScheduleSlotById(scheduleSlotId);

                if (scheduleSlot == null)
                {
                    return new Result(false);
                }

                string reservationsRaw = scheduleSlot.Reservations != null ? scheduleSlot.Reservations : "";
                foreach (Reservation reservation in reservations)
                {
                    reservationsRaw += reservationsRaw != "" ? string.Format(";{0}", reservation.Id) : reservation.Id.ToString();
                }

                scheduleSlot.Reservations = reservationsRaw;
                databaseContext.ScheduleSlots.Update(scheduleSlot);
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result AddSchedule(Schedule schedule)
        {
            try
            {
                ICollection<Schedule> schedules = databaseContext.Schedules.ToList();
                int id = schedules.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = schedules.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                schedule.Id = id;
                databaseContext.Schedules.Add(schedule);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public int GenerateSchedule(bool writeToDatabase)
        {
            try
            {
                Schedule schedule = new Schedule()
                {
                    MovieSchedule = ""
                };

                ICollection<Schedule> schedules = databaseContext.Schedules.ToList();
                int id = schedules.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = schedules.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                schedule.Id = id;
                if (writeToDatabase)
                {
                    databaseContext.Schedules.Add(schedule);
                    databaseContext.SaveChanges();
                }
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public Schedule GetScheduleById(int scheduleId)
        {
            return databaseContext.Schedules.FirstOrDefault(x => x.Id == scheduleId);
        }

        public ICollection<Schedule> GetSchedules()
        {
            return databaseContext.Schedules.ToList();
        }

        public ScheduleSlot GetScheduleSlotById(int scheduleSlotId)
        {
            return databaseContext.ScheduleSlots.FirstOrDefault(x => x.Id == scheduleSlotId);
        }

        public Result RemoveSchedule(int scheduleId)
        {
            try
            {
                Schedule schedule = databaseContext.Schedules.FirstOrDefault(x => x.Id == scheduleId);

                if (schedule == null)
                {
                    return new Result(false);
                }

                string[] scheduleSlotIds = schedule.MovieSchedule.Split(';', StringSplitOptions.RemoveEmptyEntries);
                List<ScheduleSlot> removedScheduleSlots = new List<ScheduleSlot>();
                foreach (string id in scheduleSlotIds)
                {
                    ScheduleSlot scheduleSlot = GetScheduleSlotById(int.Parse(id));

                    if (scheduleSlot != null)
                    {
                        removedScheduleSlots.Add(scheduleSlot);
                    }
                }

                databaseContext.ScheduleSlots.RemoveRange(removedScheduleSlots);
                databaseContext.Schedules.Remove(schedule);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result RemoveScheduledMovie(int roomId, int scheduleSlotId)
        {
            try
            {
                ScheduleSlot scheduleSlot = databaseContext.ScheduleSlots.FirstOrDefault(x => x.Id == scheduleSlotId);

                if (scheduleSlot == null)
                {
                    return new Result(false);
                }

                databaseContext.ScheduleSlots.Remove(scheduleSlot);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result ScheduleMovie(int roomId, ScheduleSlot scheduleSlot)
        {
            try
            {
                Room room = roomRepository.GetRoomById(roomId);

                if (room == null)
                {
                    return new Result(false);
                }

                // If scheduled slot overlaps with already scheduled movies return false
                if (databaseContext.ScheduleSlots.Any(x => x.ScheduleId == room.ScheduleId && x.Start < scheduleSlot.End && scheduleSlot.Start < x.End))
                {
                    return new Result(false);
                }

                ICollection<ScheduleSlot> dbScheduleSlots = databaseContext.ScheduleSlots.ToList();
                int id = dbScheduleSlots.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = dbScheduleSlots.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                scheduleSlot.Id = id;

                Schedule schedule = GetScheduleById(room.ScheduleId);

                if (schedule == null)
                {
                    return new Result(false);
                }
                schedule.MovieSchedule += string.IsNullOrWhiteSpace(schedule.MovieSchedule) ? id.ToString() : ";" + id;

                databaseContext.Schedules.Update(schedule);
                databaseContext.ScheduleSlots.Add(scheduleSlot);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }
    }
}
