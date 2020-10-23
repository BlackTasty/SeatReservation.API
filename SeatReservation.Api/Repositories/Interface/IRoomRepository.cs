using SeatReservation.Api.Util;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface IRoomRepository
    {
        ICollection<Room> GetRooms();

        Room GetRoomByName(string name);

        Room GetRoomById(int id);

        Result AddRoom(Room room, RoomPlan roomPlan, ICollection<SeatPosition> seatPositions);
        
        Result UpdateRoom(Room room, RoomPlan roomPlan, ICollection<SeatPosition> added, ICollection<SeatPosition> updated);
        
        Result RemoveRoom(int roomId);
        
        Result SetOpenStatus(int roomId, bool isOpen);

        RoomPlan GetRoomPlan(int roomId);

        Schedule GetSchedule(int roomId);

        SeatPosition GetSeatPositionById(int seatPositionId);
    }
}
