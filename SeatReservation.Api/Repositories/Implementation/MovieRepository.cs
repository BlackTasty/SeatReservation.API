using SeatReservation.Api.Util;
using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DatabaseContext databaseContext;

        public MovieRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Result AddMovie(Movie movie)
        {
            try
            {
                databaseContext.Movies.Add(movie);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Movie GetById(int id)
        {
            Movie movie = databaseContext.Movies.FirstOrDefault(x => x.Id == id);
            return movie;
        }

        public Result ArchiveMovie(int movieId)
        {
            try
            {
                Movie movie = databaseContext.Movies.FirstOrDefault(x => x.Id == movieId);

                if (movie == null)
                {
                    return new Result(false);
                }

                movie.IsArchived = true;
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public ICollection<Genre> GetGenres()
        {
            return databaseContext.Genres.ToList();
        }

        public Genre GetGenreById(int genreId)
        {
            return databaseContext.Genres.FirstOrDefault(x => x.Id == genreId);
        }

        public ICollection<Movie> GetMovies(bool showArchived)
        {
            if (!showArchived)
            {
                return databaseContext.Movies.Where(x => !x.IsArchived).ToList();
            }
            else
            {
                return databaseContext.Movies.ToList();
            }
        }

        public ICollection<Movie> SearchMoviesByTitle(string title)
        {
            return databaseContext.Movies.Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Result UpdateMovie(Movie movie)
        {
            try
            {
                databaseContext.Movies.Update(movie);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public ICollection<Movie> GetFeaturedMovies()
        {
            return databaseContext.Movies.Where(x => x.IsFeatured).ToList();
        }
    }
}
