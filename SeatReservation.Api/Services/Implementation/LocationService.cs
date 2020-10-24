using AutoMapper;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Interface;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;
        private readonly IRoomService roomService;
        private readonly IParser parser;

        public LocationService(ILocationRepository locationRepository, IRoomService roomService, IParser parser)
        {
            this.locationRepository = locationRepository;
            this.roomService = roomService;
            this.parser = parser;
        }

        public Result AddLocation(LocationDto location)
        {
            return locationRepository.AddLocation(parser.ToLocation(location));
        }

        public LocationDto GetLocationById(int locationId)
        {
            return parser.ToLocationDto(locationRepository.GetLocationById(locationId));
        }

        public ICollection<LocationDto> GetLocations(bool showShutdown)
        {
            List<LocationDto> locations = new List<LocationDto>();
            foreach (Location location in locationRepository.GetLocations(showShutdown))
            {
                locations.Add(parser.ToLocationDto(location));
            }
            return locations;
        }

        public Result ReopenLocation(int locationId)
        {
            return locationRepository.ReopenLocation(locationId);
        }

        public Result ShutdownLocation(int locationId)
        {
            return locationRepository.ShutdownLocation(locationId);
        }

        public Result UpdateLocation(LocationDto location)
        {
            return locationRepository.UpdateLocation(parser.ToLocation(location), GetUpdatedRoomAssignment(location));
        }

        private RoomAssignment GetUpdatedRoomAssignment(LocationDto location)
        {
            RoomAssignment target = locationRepository.GetRoomAssignmentForLocation(location.Id);
            if (target != null)
            {
                string roomIds = "";
                foreach (RoomDto room in location.Rooms)
                {
                    roomIds += roomIds != "" ? string.Format(";{0}", room.Id) : room.Id.ToString();
                }
                target.RoomIds = roomIds;
            }

            return target;
        }

        public ICollection<RoomDto> GetUnassignedRooms()
        {
            List<Location> locations = locationRepository.GetLocations(false).ToList();
            List<RoomDto> unassignedRooms = roomService.GetRooms().ToList();
            foreach (Location location in locationRepository.GetLocations(false))
            {
                foreach (RoomDto assignedRoom in GetAssignedRoomsForLocation(location.Id))
                {
                    int index = unassignedRooms.FindIndex(x => x.Id == assignedRoom.Id);
                    if (index >= 0)
                    {
                        unassignedRooms.RemoveAt(index);
                    }
                }
            }

            return unassignedRooms;
        }

        public ICollection<RoomDto> GetAssignedRoomsForLocation(int locationId)
        {
            return parser.GetAssignedRoomsForLocation(locationId);
        }

        public LocationDto GetLocationByRoomId(int id)
        {
            foreach (LocationDto location in GetLocations(false))
            {
                if (location.Rooms.FirstOrDefault(x => x.Id == id) != null)
                {
                    return location;
                }
            }

            return null;
        }
    }
}
