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
            return movieRepository.AddMovie(parser.ToMovie(movie));
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
            return movieRepository.UpdateMovie(parser.ToMovie(movie));
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
    }
}
