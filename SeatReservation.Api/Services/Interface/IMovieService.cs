using SeatReservation.Api.Util;
using SeatReservation.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface IMovieService
    {
        ICollection<MovieDto> GetMovies(bool showArchived);

        MovieDto GetMovieById(int movieId);

        ICollection<MovieDto> GetFeaturedMovies();

        ICollection<MovieDto> SearchMoviesByTitle(string title);

        Result AddMovie(MovieDto movie);

        Result UpdateMovie(MovieDto movie);

        Result ArchiveMovie(int movieId);

        ICollection<GenreDto> GetGenres();

        GenreDto GetGenreById(int genreId);

        //Result AddPerson(PersonDto person);

        ICollection<PersonDto> GetPeople();

        //Result AddStudio(StudioDto studio);

        ICollection<StudioDto> GetStudios();
    }
}
