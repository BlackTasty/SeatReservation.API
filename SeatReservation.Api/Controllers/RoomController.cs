using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Api.Util;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Services.Interface;
using Serilog;

namespace SeatReservation.Api.Controllers
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [AllowAnonymous]
        [HttpGet("getroomplan")]
        [ProducesResponseType(200)]
        public IActionResult GetRoomPlan(int roomId)
        {
            return Ok(roomService.GetRoomPlan(roomId));
        }

        [AllowAnonymous]
        [HttpGet("getschedule")]
        [ProducesResponseType(200)]
        public IActionResult GetSchedule(int roomId)
        {
            return Ok(roomService.GetSchedule(roomId));
        }

        [AllowAnonymous]
        [HttpGet("getroombyscheduleid")]
        [ProducesResponseType(200)]
        public IActionResult GetToomByScheduleId(int id)
        {
            return Ok(roomService.GetRoomByScheduleId(id));
        }

        [AllowAnonymous]
        [HttpGet("getroombyname")]
        [ProducesResponseType(200)]
        public IActionResult GetRoomByName(string name)
        {
            return Ok(roomService.GetRoomByName(name));
        }

        [AllowAnonymous]
        [HttpGet("getroombyid")]
        [ProducesResponseType(200)]
        public IActionResult GetRoomById(int id)
        {
            return Ok(roomService.GetRoomById(id));
        }

        [HttpPost("addroom")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult AddRoom([FromBody]RoomDto room)
        {
            Result result = roomService.AddRoom(room);
            if (result.Success)
            {
                return Ok(result.Id);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding room to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add room to database!");
                    return Conflict();
                }
            }
        }

        [HttpPost("updateroom")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult UpdateRoom([FromBody]RoomDto room)
        {
            Result result = roomService.UpdateRoom(room);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error updating room with id {0}!", room.Id);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to upddate room with id {0}!", room.Id);
                    return Conflict();
                }
            }
        }

        [HttpDelete("removeroom")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult RemoveRoom(int roomId)
        {
            Result result = roomService.RemoveRoom(roomId);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error deleting room with id {0}!", roomId);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to delete room with id {0}!", roomId);
                    return Conflict();
                }
            }
        }

        [AllowAnonymous]
        [HttpGet("getrooms")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult GetRooms()
        {
            return Ok(roomService.GetRooms());
        }

        [HttpGet("setopenstatus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult SetOpenStatus(int roomId, bool isOpen)
        {
            Result result = roomService.SetOpenStatus(roomId, isOpen);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error setting \"Open\" status for room with id {0}!", roomId);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to set \"Open\" status for room with id {0}!", roomId);
                    return Conflict();
                }
            }
        }

        [AllowAnonymous]
        [HttpGet("gettechnologies")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult GetTechnologies()
        {
            return Ok(roomService.GetTechnologies());
        }

        [AllowAnonymous]
        [HttpGet("gettechnologybyid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult GetTechnologyById(int id)
        {
            return Ok(roomService.GetTechnologyById(id));
        }
    }
}