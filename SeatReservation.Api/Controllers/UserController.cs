using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Services.Interface;

namespace SeatReservation.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserCredentialDto userParam)
        {
            var user = userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public IActionResult Add([FromBody]UserDto user)
        {
            int id = userService.Add(user);
            if (id > 0)
            {
                return Ok(id);
            }
            else
            {
                // Return id communicates error. -1 = Unexpected server error; -2 = User with this username exists already
                return Ok(id);
            }
        }

        [HttpPost("update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public IActionResult Update([FromBody]UserDto user)
        {
            if (userService.Update(user))
            {
                return Ok(true);
            }
            else
            {
                return Conflict(false);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public IActionResult Delete(int userId)
        {
            if (userService.Delete(userId))
            {
                return Ok(true);
            }
            else
            {
                return Conflict(false);
            }
        }

        [HttpPost("setpermissions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public IActionResult SetPermissions([FromBody]UserPermissionDto userPermissions)
        {
            if (userService.SetPermissions(userPermissions))
            {
                return Ok(true);
            }
            else
            {
                return Conflict(false);
            }
        }

        [AllowAnonymous]
        [HttpGet("getpermissions")]
        [ProducesResponseType(200)]
        public IActionResult GetPermissions(int userId)
        {
            return Ok(userService.GetPermissions(userId));
        }

        [HttpGet("getavailablepermissions")]
        [ProducesResponseType(200)]
        public IActionResult GetAvailablePermissions()
        {
            return Ok(userService.GetAvailablePermissions());
        }

        [HttpGet("get")]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            return Ok(userService.Get());
        }

        [HttpGet("getbyid")]
        [ProducesResponseType(200)]
        public IActionResult GetById(int userId)
        {
            return Ok(userService.GetById(userId));
        }
    }
}