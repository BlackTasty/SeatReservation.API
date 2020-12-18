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
        private readonly IScheduleRepository scheduleRepository;

        public MovieRepository(DatabaseContext databaseContext, IScheduleRepository scheduleRepository)
        {
            this.databaseContext = databaseContext;
            this.scheduleRepository = scheduleRepository;
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

        public Movie GetMovieById(int movieId)
        {
            return databaseContext.Movies.FirstOrDefault(x => x.Id == movieId);
        }

        public int AddPerson(Person person)
        {
            try
            {
                ICollection<Person> people = GetPeople();
                int id = people.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = people.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                person.Id = id;
                databaseContext.People.Add(person);
                databaseContext.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public ICollection<Person> GetPeople()
        {
            return databaseContext.People.ToList();
        }

        public int AddStudio(Studio studio)
        {
            try
            {
                ICollection<Studio> studios = GetStudios();
                int id = studios.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = studios.Any(x => x.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                studio.Id = id;
                databaseContext.Studios.Add(studio);
                databaseContext.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public ICollection<Studio> GetStudios()
        {
            return databaseContext.Studios.ToList();
        }

        public Person GetPersonById(int id)
        {
            return databaseContext.People.FirstOrDefault(x => x.Id == id);
        }

        public Studio GetStudioById(int id)
        {
            return databaseContext.Studios.FirstOrDefault(x => x.Id == id);
        }

        public Movie GetMovieByScheduleSlotId(int scheduleSlotId)
        {
            ScheduleSlot scheduleSlot = scheduleRepository.GetScheduleSlotById(scheduleSlotId);
            return scheduleSlot != null ? GetMovieById(scheduleSlot.MovieId) : null;
        }
    }
}
