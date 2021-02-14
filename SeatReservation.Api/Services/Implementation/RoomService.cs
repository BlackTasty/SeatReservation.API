using AutoMapper;
using SeatReservation.Api.Util;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IScheduleRepository scheduleRepository;
        private readonly ISeatTypeRepository seatTypeRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;
        private readonly IParser parser;

        public RoomService(IRoomRepository roomRepository, IScheduleRepository scheduleRepository, ISeatTypeRepository seatTypeRepository, 
            ILocationRepository locationRepository, IMapper mapper, IParser parser)
        {
            this.roomRepository = roomRepository;
            this.scheduleRepository = scheduleRepository;
            this.seatTypeRepository = seatTypeRepository;
            this.locationRepository = locationRepository;
            this.mapper = mapper;
            this.parser = parser;
        }

        public Result AddRoom(RoomCreationDto roomCreation)
        {
            Result result = roomRepository.AddRoom(parser.ToRoom(roomCreation.Room), parser.ToRoomPlan(roomCreation.Room.RoomPlan, true),
                        mapper.Map<ICollection<SeatPosition>>(roomCreation.Room.RoomPlan.Seats));
            if (result.Id > 0)
            {
                RoomAssignment roomAssignment = locationRepository.GetRoomAssignmentForLocation(roomCreation.LocationId);
                roomAssignment.RoomIds += !string.IsNullOrWhiteSpace(roomAssignment.RoomIds) ? ";" + result.Id : result.Id.ToString();
                locationRepository.UpdateRoomAssignment(roomAssignment);
            }
            return result;
        }

        public RoomDto GetRoomByName(string name)
        {
            return parser.ToRoomDto(roomRepository.GetRoomByName(name));
        }

        public RoomDto GetRoomById(int id)
        {
            return parser.ToRoomDto(roomRepository.GetRoomById(id));
        }

        public RoomPlanDto GetRoomPlan(int roomId)
        {
            return parser.ToRoomPlanDto(roomRepository.GetRoomPlan(roomId));
        }

        public ICollection<RoomDto> GetRooms()
        {
            List<RoomDto> rooms = new List<RoomDto>();
            foreach (Room room in roomRepository.GetRooms())
            {
                rooms.Add(parser.ToRoomDto(room));
            }

            return rooms;
        }

        public ScheduleDto GetSchedule(int roomId)
        {
            return parser.ToScheduleDto(roomRepository.GetSchedule(roomId));
        }

        public Result RemoveRoom(int roomId)
        {
            return roomRepository.RemoveRoom(roomId);
        }

        public Result SetOpenStatus(int roomId, bool isOpen)
        {
            return roomRepository.SetOpenStatus(roomId, isOpen);
        }

        public Result UpdateRoom(RoomDto room)
        {
            ICollection<SeatPosition> seatPositions = mapper.Map<ICollection<SeatPosition>>(room.RoomPlan.Seats);
            return roomRepository.UpdateRoom(parser.ToRoom(room), parser.ToRoomPlan(room.RoomPlan, false), seatPositions.Where(x=> x.Id == 0).ToList(), seatPositions.Where(x => x.Id != 0).ToList());
        }

        public ICollection<RoomTechnologyDto> GetTechnologies()
        {
            return mapper.Map<ICollection<RoomTechnologyDto>>(roomRepository.GetTechnologies());
        }

        public RoomTechnologyDto GetTechnologyById(int technologyId)
        {
            return mapper.Map<RoomTechnologyDto>(roomRepository.GetTechnologyById(technologyId));
        }

        public RoomDto GetRoomByScheduleId(int scheduleId)
        {
            return parser.ToRoomDto(roomRepository.GetRoomByScheduleId(scheduleId));
        }
    }
}
