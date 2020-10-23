using SeatReservation.Api.Util;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies(bool showArchived);

        ICollection<Movie> GetFeaturedMovies();

        ICollection<Movie> SearchMoviesByTitle(string title);

        Result AddMovie(Movie movie);

        Result UpdateMovie(Movie movie);

        Result ArchiveMovie(int movieId);

        ICollection<Genre> GetGenres();

        Genre GetGenreById(int genreId);

        Movie GetById(int id);
    }
}
