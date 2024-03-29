﻿using System;
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
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [AllowAnonymous]
        [HttpGet("getschedules")]
        [ProducesResponseType(200)]
        public IActionResult GetSchedules()
        {
            return Ok(scheduleService.GetSchedules());
        }

        [AllowAnonymous]
        [HttpGet("getscheduleslotbyid")]
        [ProducesResponseType(200)]
        public IActionResult GetScheduleSlotById(int id)
        {
            return Ok(scheduleService.GetScheduleSlotById(id));
        }

        [HttpPost("schedulemovie")]
        [ProducesResponseType(200)]
        public IActionResult ScheduleMovie([FromBody]RoomScheduleSlotDto roomScheduleSlot)
        {
            Result result = scheduleService.ScheduleMovie(roomScheduleSlot.RoomId, roomScheduleSlot.ScheduleSlot);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding movie to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add movie to database!");
                    return Conflict();
                }
            }
        }

        [HttpGet("generateschedule")]
        [ProducesResponseType(200)]
        public IActionResult GenerateSchedule(bool writeToDatabase)
        {
            return Ok(scheduleService.GenerateSchedule(writeToDatabase));
        }

        [HttpPost("updateschedule")]
        [ProducesResponseType(200)]
        public IActionResult UpdateSchedule([FromBody]RoomScheduleSlotDto roomScheduleSlot)
        {
            Result result = scheduleService.ScheduleMovie(roomScheduleSlot.RoomId, roomScheduleSlot.ScheduleSlot);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding movie to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add movie to database!");
                    return Conflict();
                }
            }
        }

        [HttpDelete("removescheduledmovie")]
        [ProducesResponseType(200)]
        public IActionResult RemoveScheduledMovie(int roomId, int scheduleSlotId)
        {
            Result result = scheduleService.RemoveScheduledMovie(roomId, scheduleSlotId);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding movie to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add movie to database!");
                    return Conflict();
                }
            }
        }

        [HttpGet("getdateswithmovies")]
        [ProducesResponseType(200)]
        public IActionResult GetDatesWithMovies()
        {
            return Ok(scheduleService.GetDatesWithMovies());
        }

        [HttpPost("copyschedule")]
        [ProducesResponseType(200)]
        public IActionResult CopySchedule([FromBody]ScheduleCopyTargetDto scheduleCopyTarget)
        {
            Result result = scheduleService.CopySchedule(scheduleCopyTarget);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error copying schedule!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to copy schedule!");
                    return Conflict();
                }
            }
        }
    }
}