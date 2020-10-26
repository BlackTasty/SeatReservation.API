using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [AllowAnonymous]
        [HttpGet("getreservations")]
        [ProducesResponseType(200)]
        public IActionResult GetReservations()
        {
            return Ok(reservationService.GetReservations());
        }

        [AllowAnonymous]
        [HttpGet("getreservationsforschedule")]
        [ProducesResponseType(200)]
        public IActionResult GetReservationsForSchedule(int id)
        {
            return Ok(reservationService.GetReservationsForSchedule(id));
        }

        [AllowAnonymous]
        [HttpPost("addreservation")]
        [ProducesResponseType(200)]
        public IActionResult AddReservation([FromBody]ICollection<ReservationDto> reservations)
        {
            return Ok(reservationService.AddReservation(reservations));
        }

        [AllowAnonymous]
        [HttpGet("cancelreservation")]
        [ProducesResponseType(200)]
        public IActionResult CancelReservation(int reservation, int user)
        {
            return Ok(reservationService.CancelReservation(reservation, user));
        }
    }
}
