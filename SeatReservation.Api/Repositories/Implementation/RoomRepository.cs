using SeatReservation.Api.Util;
using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeatReservation.Api.DTO;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DatabaseContext databaseContext;

        public RoomRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Result AddRoom(Room room, RoomPlan roomPlan, ICollection<SeatPosition> seatPositions)
        {
            try
            {
                ICollection<SeatPosition> dbSeatPositions = databaseContext.SeatPositions.ToList();
                int id = dbSeatPositions.Count + 1;
                bool duplicateId = false;
                List<SeatPosition> addedSeatPositions = new List<SeatPosition>();
                foreach (SeatPosition seatPosition in seatPositions)
                {
                    do
                    {
                        duplicateId = dbSeatPositions.Any(x => x.Id == id);
                        if (duplicateId)
                        {
                            id++;
                        }
                    } while (duplicateId);
                    seatPosition.Id = id;
                    addedSeatPositions.Add(seatPosition);
                    id++;
                }
                databaseContext.SeatPositions.AddRange(addedSeatPositions);
                databaseContext.SaveChanges();

                ICollection<RoomPlan> roomPlans = databaseContext.RoomPlans.ToList();
                id = roomPlans.Count + 1;
                do
                {
                    duplicateId = roomPlans.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                roomPlan.Id = id;
                roomPlan.Seats = SeatPositionsListToString(seatPositions);

                databaseContext.RoomPlans.Add(roomPlan);
                databaseContext.SaveChanges();

                ICollection<Room> rooms = GetRooms();
                id = rooms.Count + 1;
                do
                {
                    duplicateId = rooms.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                room.Id = id;
                room.RoomPlanId = id;

                databaseContext.Rooms.Add(room);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        private string SeatPositionsListToString(ICollection<SeatPosition> seatPositions)
        {
            string seatPositionsString = "";

            foreach (SeatPosition seatPosition in seatPositions)
            {
                seatPositionsString += seatPositionsString == "" ? seatPosition.Id.ToString() : ";" + seatPosition.Id;
            }

            return seatPositionsString;
        }

        public Room GetRoomByName(string name)
        {
            return databaseContext.Rooms.FirstOrDefault(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public RoomPlan GetRoomPlan(int roomId)
        {
            Room room = databaseContext.Rooms.FirstOrDefault(x => x.Id == roomId);

            if (room == null)
            {
                return null;
            }

            return databaseContext.RoomPlans.FirstOrDefault(x => x.Id == room.RoomPlanId);
        }

        public ICollection<Room> GetRooms()
        {
            return databaseContext.Rooms.ToList();
        }

        public Schedule GetSchedule(int roomId)
        {
            Room room = databaseContext.Rooms.FirstOrDefault(x => x.Id == roomId);

            if (room == null)
            {
                return null;
            }

            return databaseContext.Schedules.FirstOrDefault(x => x.Id == room.ScheduleId);
        }

        public Result RemoveRoom(int roomId)
        {
            try
            {
                Room room = databaseContext.Rooms.FirstOrDefault(x => x.Id == roomId);

                if (room == null)
                {
                    return new Result(false);
                }

                databaseContext.Rooms.Remove(room);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result SetOpenStatus(int roomId, bool isOpen)
        {
            try
            {
                Room room = databaseContext.Rooms.FirstOrDefault(x => x.Id == roomId);

                if (room == null)
                {
                    return new Result(false);
                }

                room.IsOpen = isOpen;
                databaseContext.Rooms.Update(room);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result UpdateRoom(Room room, RoomPlan roomPlan, ICollection<SeatPosition> added, ICollection<SeatPosition> updated)
        {
            try
            {
                string seatPositionIds = "";
                foreach (SeatPosition seatPosition in updated)
                {
                    SeatPosition dbSeatPosition = GetSeatPositionById(seatPosition.Id);
                     if (dbSeatPosition != null)
                    {
                        dbSeatPosition.Column = seatPosition.Column;
                        dbSeatPosition.Rotation = seatPosition.Rotation;
                        dbSeatPosition.Row = seatPosition.Row;
                        dbSeatPosition.SeatTypeId = seatPosition.SeatTypeId;
                        databaseContext.SeatPositions.Update(dbSeatPosition);
                        seatPositionIds += seatPositionIds == "" ? seatPosition.Id.ToString() : ";" + seatPosition.Id;
                    }
                }

                if (added.Count > 0)
                {
                    ICollection<SeatPosition> dbSeatPositions = databaseContext.SeatPositions.ToList();
                    int id = dbSeatPositions.Count + 1;
                    bool duplicateId = false;
                    foreach (SeatPosition seatPosition in added)
                    {
                        do
                        {
                            duplicateId = dbSeatPositions.Any(x => x.Id == id);
                            if (duplicateId)
                            {
                                id++;
                            }
                        } while (duplicateId);
                        seatPosition.Id = id;
                        seatPositionIds += seatPositionIds == "" ? id.ToString() : ";" + id;
                        id++;
                    }

                    databaseContext.SeatPositions.AddRange(added);
                }

                string[] oldSeatPositionIds = roomPlan.Seats.Split(';', StringSplitOptions.RemoveEmptyEntries); 
                foreach (string seatPositionId in oldSeatPositionIds)
                {
                    if (!seatPositionIds.Contains(seatPositionId))
                    {
                        SeatPosition seatPosition = databaseContext.SeatPositions.FirstOrDefault(x => x.Id == int.Parse(seatPositionId));

                        if (seatPosition != null)
                        {
                            databaseContext.SeatPositions.Remove(seatPosition);
                        }
                    }
                }

                roomPlan.Seats = seatPositionIds;
                databaseContext.RoomPlans.Update(roomPlan);

                room.RoomPlanId = roomPlan.Id;
                databaseContext.Rooms.Update(room);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public SeatPosition GetSeatPositionById(int seatPositionId)
        {
            return databaseContext.SeatPositions.FirstOrDefault(x => x.Id == seatPositionId);
        }

        public Room GetRoomById(int id)
        {
            return databaseContext.Rooms.FirstOrDefault(x => x.Id == id);
        }
    }
}
