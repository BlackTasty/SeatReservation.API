﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}