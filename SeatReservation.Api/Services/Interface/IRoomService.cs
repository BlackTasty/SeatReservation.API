using SeatReservation.Api.Util;
using SeatReservation.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface IRoomService
    {
        ICollection<RoomDto> GetRooms();

        RoomDto GetRoomByName(string name);

        RoomDto GetRoomById(int id);

        Result AddRoom(RoomDto room);

        Result UpdateRoom(RoomDto room);

        Result RemoveRoom(int roomId);

        Result SetOpenStatus(int roomId, bool isOpen);

        RoomPlanDto GetRoomPlan(int roomId);

        ScheduleDto GetSchedule(int roomId);

        RoomDto GetRoomByScheduleId(int scheduleId);

        ICollection<RoomTechnologyDto> GetTechnologies();

        RoomTechnologyDto GetTechnologyById(int technologyId);
    }
}
