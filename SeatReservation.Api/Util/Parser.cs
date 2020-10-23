﻿using AutoMapper;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Util
{
    public class Parser : IParser
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IMovieRepository movieRepository;
        private readonly ISeatTypeRepository seatTypeRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public Parser(IScheduleRepository scheduleRepository, IRoomRepository roomRepository, IMovieRepository movieRepository,
            ISeatTypeRepository seatTypeRepository, IReservationRepository reservationRepository, IMapper mapper)
        {
            this.scheduleRepository = scheduleRepository;
            this.roomRepository = roomRepository;
            this.movieRepository = movieRepository;
            this.seatTypeRepository = seatTypeRepository;
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;

        }

        public Schedule ToSchedule(ScheduleDto scheduleDto)
        {
            return new Schedule()
            {
                Id = scheduleDto.Id,
                MovieSchedule = MovieScheduleToString(scheduleDto.MovieSchedule)
            };
        }

        public ScheduleDto ToScheduleDto(Schedule schedule)
        {
            return new ScheduleDto()
            {
                Id = schedule?.Id ?? -1,
                MovieSchedule = schedule != null ? MovieScheduleToList(schedule.MovieSchedule) : null
            };
        }

        public ScheduleSlot ToScheduleSlot(ScheduleSlotDto scheduleSlotDto)
        {
            return new ScheduleSlot()
            {
                Id = scheduleSlotDto.Id,
                Start = scheduleSlotDto.Start.AddHours(2),
                End = scheduleSlotDto.End.AddHours(2),
                MovieId = scheduleSlotDto.MovieId,
                Reservations = ReservationsListToString(scheduleSlotDto.Reservations)
            };
        }

        public ScheduleSlotDto ToScheduleSlotDto(ScheduleSlot scheduleSlot)
        {
            return new ScheduleSlotDto()
            {
                Id = scheduleSlot.Id,
                Start = scheduleSlot.Start,
                End = scheduleSlot.End,
                Movie = ToMovieDto(movieRepository.GetById(scheduleSlot.MovieId)),
                MovieId = scheduleSlot.MovieId,
                Reservations = ReservationsStringToList(scheduleSlot.Reservations)
            };
        }

        public Room ToRoom(RoomDto roomDto)
        {
            return new Room()
            {
                Id = roomDto.Id,
                IsOpen = roomDto.IsOpen,
                Name = roomDto.Name,
                RoomPlanId = roomDto.RoomPlanId,
                ScheduleId = roomDto.ScheduleId
            };
        }

        public RoomDto ToRoomDto(Room room)
        {
            RoomPlanDto roomPlan = ToRoomPlanDto(roomRepository.GetRoomPlan(room.Id));
            ScheduleDto schedule = ToScheduleDto(scheduleRepository.GetScheduleById(room.ScheduleId));
            return new RoomDto()
            {
                Id = room.Id,
                IsOpen = room.IsOpen,
                Name = room.Name,
                RoomPlan = roomPlan,
                RoomPlanId = roomPlan?.Id ?? -1,
                Schedule = schedule,
                ScheduleId = schedule?.Id ?? -1
            };
        }

        public RoomPlan ToRoomPlan(RoomPlanDto roomPlanDto, bool includeZeroIds)
        {
            return new RoomPlan()
            {
                Id = roomPlanDto.Id,
                Columns = roomPlanDto.Columns,
                Rows = roomPlanDto.Rows,
                Seats = SeatPositionsListToString(roomPlanDto.Seats, includeZeroIds)
            };
        }

        public RoomPlanDto ToRoomPlanDto(RoomPlan roomPlan)
        {
            return new RoomPlanDto()
            {
                Id = roomPlan.Id,
                Columns = roomPlan.Columns,
                Rows = roomPlan.Rows,
                Seats = SeatPositionsStringToList(roomPlan.Seats)
            };
        }

        public MovieDto ToMovieDto(Movie movie)
        {
            return new MovieDto()
            {
                Id = movie.Id,
                Title = movie.Title,
                Banner = movie.Banner,
                Poster = movie.Poster,
                Logo = movie.Logo,
                Trailer = movie.Trailer,
                Description = movie.Description,
                MovieLength = movie.MovieLength,
                ReleaseDate = movie.ReleaseDate,
                IsArchived = movie.IsArchived,
                Genres = GenresStringToList(movie.Genres),
                IsFeatured = movie.IsFeatured
            };
        }

        public Movie ToMovie(MovieDto movieDto)
        {
            return new Movie()
            {
                Id = movieDto.Id,
                Title = movieDto.Title,
                Banner = movieDto.Banner,
                Poster = movieDto.Poster,
                Logo = movieDto.Logo,
                Trailer = movieDto.Trailer,
                Description = movieDto.Description,
                MovieLength = movieDto.MovieLength,
                ReleaseDate = movieDto.ReleaseDate,
                IsArchived = movieDto.IsArchived,
                Genres = GenresListToString(movieDto.Genres),
                IsFeatured = movieDto.IsFeatured
            };
        }

        private List<GenreDto> GenresStringToList(string genresString)
        {
            List<GenreDto> genres = new List<GenreDto>();
            string[] genreIds = genresString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in genreIds)
            {
                genres.Add(mapper.Map<GenreDto>(movieRepository.GetGenreById(int.Parse(id))));
            }

            return genres;
        }

        private string GenresListToString(ICollection<GenreDto> genres)
        {
            string genresString = "";

            foreach (GenreDto genre in genres)
            {
                genresString += genresString == "" ? genre.Id.ToString() : ";" + genre.Id;
            }

            return genresString;
        }

        #region List parser
        #region MovieSchedule
        private string MovieScheduleToString(ICollection<ScheduleSlotDto> scheduleSlots)
        {
            string scheduleSlotString = "";

            foreach (ScheduleSlotDto scheduleSlot in scheduleSlots)
            {
                scheduleSlotString += scheduleSlotString == "" ? scheduleSlot.Id.ToString() : ";" + scheduleSlot.Id;
            }

            return scheduleSlotString;
        }

        private List<ScheduleSlotDto> MovieScheduleToList(string scheduleSlotString)
        {
            List<ScheduleSlotDto> scheduleSlots = new List<ScheduleSlotDto>();
            string[] scheduleSlotIds = scheduleSlotString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in scheduleSlotIds)
            {
                ScheduleSlotDto scheduleSlot = ToScheduleSlotDto(scheduleRepository.GetScheduleSlotById(int.Parse(id)));
                if (scheduleSlot != null)
                {
                    scheduleSlots.Add(scheduleSlot);
                }
            }

            return scheduleSlots;
        }
        #endregion

        #region SeatPosition
        private List<SeatPositionDto> SeatPositionsStringToList(string seatPositionsString)
        {
            List<SeatPositionDto> seatPositions = new List<SeatPositionDto>();
            string[] seatPositionIds = seatPositionsString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in seatPositionIds)
            {
                SeatPositionDto seatPosition = mapper.Map<SeatPositionDto>(roomRepository.GetSeatPositionById(int.Parse(id)));
                if (seatPosition == null)
                {
                    continue;
                }
                seatPosition.SeatType = mapper.Map<SeatTypeDto>(seatTypeRepository.GetSeatTypeById(seatPosition.SeatTypeId));
                seatPositions.Add(seatPosition);
            }

            return seatPositions;
        }

        private string SeatPositionsListToString(ICollection<SeatPositionDto> seatPositions, bool includeZeroIds)
        {
            string seatPositionsString = "";

            foreach (SeatPositionDto seatPosition in seatPositions.Where(x => x.Id != 0))
            {
                seatPositionsString += seatPositionsString == "" ? seatPosition.Id.ToString() : ";" + seatPosition.Id;
            }

            if (includeZeroIds)
            {
                foreach (SeatPositionDto seatPosition in seatPositions.Where(x => x.Id == 0))
                {
                    seatPositionsString += seatPositionsString == "" ? seatPosition.Id.ToString() : ";" + seatPosition.Id;
                }
            }

            return seatPositionsString;
        }
        #endregion

        #region Reservation
        private List<ReservationDto> ReservationsStringToList(string reservationsString)
        {
            List<ReservationDto> reservations = new List<ReservationDto>();
            string[] reservationIds = reservationsString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in reservationIds)
            {
                reservations.Add(mapper.Map<ReservationDto>(reservationRepository.GetById(int.Parse(id))));
            }

            return reservations;
        }

        private string ReservationsListToString(ICollection<ReservationDto> reservations)
        {
            string reservationsString = "";

            foreach (ReservationDto reservation in reservations.Where(x => x.Id != 0))
            {
                reservationsString += reservationsString == "" ? reservation.Id.ToString() : ";" + reservation.Id;
            }

            return reservationsString;
        }
        #endregion
        #endregion
    }
}