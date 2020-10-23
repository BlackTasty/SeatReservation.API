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
    [Route("api/seat-type")]
    public class SeatTypeController : Controller
    {
        private readonly ISeatTypeService seatService;

        public SeatTypeController(ISeatTypeService seatService)
        {
            this.seatService = seatService;
        }

        [AllowAnonymous]
        [HttpGet("getseattypes")]
        [ProducesResponseType(200)]
        public IActionResult GetSeatTypes()
        {
            return Ok(seatService.GetSeatTypes());
        }

        [HttpPost("addseattype")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult AddSeatType([FromBody]SeatTypeDto seatType)
        {
            Result result = seatService.AddSeatType(seatType);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding seat type to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add seat type to database!");
                    return Conflict();
                }
            }
        }

        [HttpPost("updateseattype")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSeatType([FromBody]SeatTypeDto seatType)
        {
            Result result = seatService.UpdateSeatType(seatType);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error updating seat type with id {0}!", seatType.Id);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to upddate seat type with id {0}!", seatType.Id);
                    return Conflict();
                }
            }
        }

        [HttpDelete("removeseattype")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult RemoveSeatType(int seatTypeId)
        {
            Result result = seatService.RemoveSeatType(seatTypeId);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error deleting seat type with id {0}!", seatTypeId);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to delete seat type with id {0}!", seatTypeId);
                    return Conflict();
                }
            }
        }
    }
}
