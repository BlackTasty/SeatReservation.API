using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Services.Interface;
using SeatReservation.Api.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [AllowAnonymous]
        [HttpGet("getlocations")]
        [ProducesResponseType(200)]
        public IActionResult GetLocations(bool showShutdown)
        {
            return Ok(locationService.GetLocations(showShutdown));
        }

        [HttpPost("addlocation")]
        [ProducesResponseType(200)]
        public IActionResult AddLocation([FromBody]LocationDto location)
        {
            Result result = locationService.AddLocation(location);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding location to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add location to database!");
                    return Conflict();
                }
            }
        }

        [HttpPost("updatelocation")]
        [ProducesResponseType(200)]
        public IActionResult UpdateLocation([FromBody]LocationDto location)
        {
            Result result = locationService.UpdateLocation(location);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error updating location with id {0}!", location.Id);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to update location with id {0}!", location.Id);
                    return Conflict();
                }
            }
        }

        [HttpGet("shutdownlocation")]
        [ProducesResponseType(200)]
        public IActionResult ShutdownLocation(int id)
        {
            Result result = locationService.ShutdownLocation(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error setting shutdown flag for location with id {0}!", id);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to set shutdown flag for location with id {0}!", id);
                    return Conflict();
                }
            }
        }

        [HttpGet("reopenlocation")]
        [ProducesResponseType(200)]
        public IActionResult ReopenLocation(int id)
        {
            Result result = locationService.ReopenLocation(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error setting shutdown flag for location with id {0}!", id);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to set shutdown flag for location with id {0}!", id);
                    return Conflict();
                }
            }
        }

        [AllowAnonymous]
        [HttpGet("getlocationbyid")]
        [ProducesResponseType(200)]
        public IActionResult GetLocationById(int id)
        {
            return Ok(locationService.GetLocationById(id));
        }

        [AllowAnonymous]
        [HttpGet("getunassignedrooms")]
        [ProducesResponseType(200)]
        public IActionResult GetUnassignedRooms()
        {
            return Ok(locationService.GetUnassignedRooms());
        }

        [AllowAnonymous]
        [HttpGet("getassignedroomsforlocation")]
        [ProducesResponseType(200)]
        public IActionResult GetAssignedRoomsForLocation(int id)
        {
            return Ok(locationService.GetAssignedRoomsForLocation(id));
        }
    }
}
