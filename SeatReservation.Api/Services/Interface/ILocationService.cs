using SeatReservation.Api.DTO;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface ILocationService
    {
        ICollection<LocationDto> GetLocations(bool showShutdown);

        Result AddLocation(LocationDto location);

        Result UpdateLocation(LocationDto location);

        Result ShutdownLocation(int locationId);

        Result ReopenLocation(int locationId);

        LocationDto GetLocationById(int locationId);

        ICollection<RoomDto> GetUnassignedRooms();

        ICollection<RoomDto> GetAssignedRoomsForLocation(int locationId);
    }
}
