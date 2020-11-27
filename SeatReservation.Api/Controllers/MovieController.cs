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
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        [HttpGet("getmovies")]
        [ProducesResponseType(200)]
        public IActionResult GetMovies(bool showArchived)
        {
            return Ok(movieService.GetMovies(showArchived));
        }

        [AllowAnonymous]
        [HttpGet("getmoviebyid")]
        [ProducesResponseType(200)]
        public IActionResult GetMovieById(int id)
        {
            return Ok(movieService.GetMovieById(id));
        }

        [AllowAnonymous]
        [HttpGet("getfeaturedmovies")]
        [ProducesResponseType(200)]
        public IActionResult GetFeaturedMovies()
        {
            return Ok(movieService.GetFeaturedMovies());
        }

        [AllowAnonymous]
        [HttpGet("searchmoviesbytitle")]
        [ProducesResponseType(200)]
        public IActionResult SearchMoviesByTitle(string title)
        {
            return Ok(movieService.SearchMoviesByTitle(title));
        }

        [HttpPost("addmovie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult AddMovie([FromBody]MovieDto movie)
        {
            Result result = movieService.AddMovie(movie);
            if (result.Success)
            {
                return Ok();
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

        [HttpGet("archivemovie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult ArchiveMovie(int movieId)
        {
            Result result = movieService.ArchiveMovie(movieId);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error archiving movie with id {0}!", movieId);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to archive movie with id {0}!", movieId);
                    return Conflict();
                }
            }
        }

        [AllowAnonymous]
        [HttpGet("getgenres")]
        [ProducesResponseType(200)]
        public IActionResult GetGenres()
        {
            return Ok(movieService.GetGenres());
        }

        [AllowAnonymous]
        [HttpGet("getgenrebyid")]
        [ProducesResponseType(200)]
        public IActionResult GetGenreById(int genreId)
        {
            return Ok(movieService.GetGenreById(genreId));
        }

        [HttpPost("updatemovie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult UpdateMovie([FromBody]MovieDto movie)
        {
            Result result = movieService.UpdateMovie(movie);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error updating movie with id {0}!", movie.Id);
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to update movie with id {0}!", movie.Id);
                    return Conflict();
                }
            }
        }

        /*[HttpPost("addperson")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult AddPerson([FromBody]PersonDto person)
        {
            Result result = movieService.AddPerson(person);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding actor/director to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add actor/director to database!");
                    return Conflict();
                }
            }
        }

        [HttpPost("addstudio")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult AddStudio([FromBody]StudioDto studio)
        {
            Result result = movieService.AddStudio(studio);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                if (result.Exception != null)
                {
                    Log.Error(result.Exception, "Error adding studio to database!");
                    return StatusCode(500);
                }
                else
                {
                    Log.Warning("Unable to add studio to database!");
                    return Conflict();
                }
            }
        }*/

        [AllowAnonymous]
        [HttpGet("getpeople")]
        [ProducesResponseType(200)]
        public IActionResult GetPeople()
        {
            return Ok(movieService.GetPeople());
        }

        [AllowAnonymous]
        [HttpGet("getstudios")]
        [ProducesResponseType(200)]
        public IActionResult GetStudios()
        {
            return Ok(movieService.GetStudios());
        }
    }
}