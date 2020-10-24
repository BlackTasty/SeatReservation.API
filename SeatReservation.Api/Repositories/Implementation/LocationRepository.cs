using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DatabaseContext databaseContext;

        public LocationRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Result AddLocation(Location location)
        {
            try
            {
                var added = databaseContext.Locations.Add(location);
                databaseContext.SaveChanges();

                databaseContext.RoomAssignments.Add(new RoomAssignment()
                {
                    LocationId = added.Entity.Id,
                    RoomIds = ""
                });
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Location GetLocationById(int locationId)
        {
            return databaseContext.Locations.FirstOrDefault(x => x.Id == locationId);
        }

        public ICollection<Location> GetLocations(bool showShutdown)
        {
            return showShutdown ? databaseContext.Locations.ToList() : databaseContext.Locations.Where(x => !x.IsShutdown).ToList();
        }

        public RoomAssignment GetRoomAssignmentForLocation(int locationId)
        {
            return databaseContext.RoomAssignments.FirstOrDefault(x => x.LocationId == locationId);
        }

        public Result ReopenLocation(int locationId)
        {
            return ToggleLocationShutdown(locationId, false);
        }

        public Result ShutdownLocation(int locationId)
        {
            return ToggleLocationShutdown(locationId, true);
        }

        public Result UpdateLocation(Location location, RoomAssignment roomAssignment)
        {
            try
            {
                databaseContext.Locations.Update(location);
                if (roomAssignment != null)
                {
                    databaseContext.RoomAssignments.Update(roomAssignment);
                }
                else
                {
                    Log.Warning("Error updating room assignment for location id {0} because there is no assignment created! Creating assignment...", location.Id);
                    databaseContext.RoomAssignments.Add(new RoomAssignment()
                    {
                        LocationId = location.Id,
                        RoomIds = ""
                    });
                }
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        private Result ToggleLocationShutdown(int locationId, bool isShutdown)
        {
            try
            {
                Location location = GetLocationById(locationId);

                if (location == null)
                {
                    return new Result(false);
                }

                location.IsShutdown = isShutdown;
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
