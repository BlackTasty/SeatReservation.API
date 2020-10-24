using SeatReservation.Api.Models;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations(bool showShutdown);

        Result AddLocation(Location location);

        Result UpdateLocation(Location location, RoomAssignment roomAssignment);

        Result ShutdownLocation(int locationId);

        Result ReopenLocation(int locationId);

        Location GetLocationById(int locationId);

        RoomAssignment GetRoomAssignmentForLocation(int locationId);
    }
}
