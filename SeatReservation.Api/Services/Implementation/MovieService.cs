using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using SeatReservation.Api.Util;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace SeatReservation.Api.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;
        private readonly IParser parser;

        public MovieService(IMovieRepository movieRepository, IMapper mapper, IParser parser)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
            this.parser = parser;
        }

        public Result AddMovie(MovieDto movie)
        {
            return movieRepository.AddMovie(parser.ToMovie(CheckPeopleAndStudios(movie)));
        }

        public Result ArchiveMovie(int movieId)
        {
            return movieRepository.ArchiveMovie(movieId);
        }

        public ICollection<MovieDto> GetMovies(bool showArchived)
        {
            List<MovieDto> movies = new List<MovieDto>();
            foreach (Movie movie in movieRepository.GetMovies(showArchived))
            {
                movies.Add(parser.ToMovieDto(movie));
            }
            return movies;
        }

        public ICollection<MovieDto> SearchMoviesByTitle(string title)
        {
            List<MovieDto> movies = new List<MovieDto>();
            foreach (Movie movie in movieRepository.SearchMoviesByTitle(title))
            {
                movies.Add(parser.ToMovieDto(movie));
            }
            return movies;
        }

        public Result UpdateMovie(MovieDto movie)
        {
            return movieRepository.UpdateMovie(parser.ToMovie(CheckPeopleAndStudios(movie)));
        }

        public ICollection<GenreDto> GetGenres()
        {
            return mapper.Map<ICollection<GenreDto>>(movieRepository.GetGenres());
        }

        public GenreDto GetGenreById(int genreId)
        {
            return mapper.Map<GenreDto>(movieRepository.GetGenreById(genreId));
        }

        public ICollection<MovieDto> GetFeaturedMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();
            foreach (Movie movie in movieRepository.GetFeaturedMovies())
            {
                movies.Add(parser.ToMovieDto(movie));
            }
            return movies;
        }

        public MovieDto GetMovieById(int movieId)
        {
            return parser.ToMovieDto(movieRepository.GetMovieById(movieId));
        }

        /*public Result AddPerson(PersonDto person)
        {
            return movieRepository.AddPerson(parser.ToPerson(person));
        }

        public Result AddStudio(StudioDto studio)
        {
            return movieRepository.AddStudio(parser.ToStudio(studio));
        }*/

        public ICollection<PersonDto> GetPeople()
        {
            List<PersonDto> peopleDto = new List<PersonDto>();
            foreach (Person person in movieRepository.GetPeople())
            {
                peopleDto.Add(parser.ToPersonDto(person));
            }

            return peopleDto;
        }

        public ICollection<StudioDto> GetStudios()
        {
            List<StudioDto> studiosDto = new List<StudioDto>();
            foreach (Studio studio in movieRepository.GetStudios())
            {
                studiosDto.Add(parser.ToStudioDto(studio));
            }

            return studiosDto;
        }

        private MovieDto CheckPeopleAndStudios(MovieDto movieDto)
        {
            foreach (StudioDto studio in movieDto.Studios.Where(x => x.Id == 0))
            {
                int id = movieRepository.AddStudio(parser.ToStudio(studio));

                if (id > 0)
                {
                    studio.Id = id;
                }
                else
                {
                    Log.Warning("Unable to add studio!");
                }
            }

            List<PersonDto> addedPeople = new List<PersonDto>();

            foreach (PersonDto person in movieDto.Actors.Where(x => x.Id == 0))
            {
                int id = movieRepository.AddPerson(parser.ToPerson(person));

                if (id > 0)
                {
                    person.Id = id;
                    addedPeople.Add(person);
                }
                else
                {
                    Log.Warning("Unable to add actor!");
                }
            }

            foreach (PersonDto person in movieDto.Directors.Where(x => x.Id == 0))
            {
                PersonDto existingPerson = addedPeople.FirstOrDefault(x => x.UniqueAddId == person.UniqueAddId);
                if (existingPerson != null)
                {
                    person.Id = existingPerson.Id;
                }
                else
                {
                    int id = movieRepository.AddPerson(parser.ToPerson(person));

                    if (id > 0)
                    {
                        person.Id = id;
                    }
                    else
                    {
                        Log.Warning("Unable to add director!");
                    }
                }
            }

            return movieDto;
        }
    }
}
