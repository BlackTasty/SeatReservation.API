using AutoMapper;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
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
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;

        public Parser(IScheduleRepository scheduleRepository, IRoomRepository roomRepository, IMovieRepository movieRepository,
            ISeatTypeRepository seatTypeRepository, IReservationRepository reservationRepository, ILocationRepository locationRepository, 
            IMapper mapper)
        {
            this.scheduleRepository = scheduleRepository;
            this.roomRepository = roomRepository;
            this.movieRepository = movieRepository;
            this.seatTypeRepository = seatTypeRepository;
            this.reservationRepository = reservationRepository;
            this.locationRepository = locationRepository;
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
                Start = scheduleSlotDto.Start.ToLocalTime(),
                End = scheduleSlotDto.End.ToLocalTime(),
                MovieId = scheduleSlotDto.MovieId,
                Reservations = ReservationsListToString(scheduleSlotDto.Reservations),
                ScheduleId = scheduleSlotDto.ScheduleId
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
                Reservations = ReservationsStringToList(scheduleSlot.Reservations),
                ScheduleId = scheduleSlot.ScheduleId
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
                ScheduleId = roomDto.ScheduleId,
                TechnologyId = roomDto.Technology == null ? 1 : roomDto.Technology.Id
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
                ScheduleId = schedule?.Id ?? -1,
                Technology = mapper.Map<RoomTechnologyDto>(roomRepository.GetTechnologyById(room.TechnologyId)),
                TechnologyId = room.TechnologyId
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
                IsFeatured = movie.IsFeatured,
                Actors = PeopleStringToList(movie.Actors),
                Directors = PeopleStringToList(movie.Directors),
                Studios = StudiosStringToList(movie.Studios)
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
                ReleaseDate = movieDto.ReleaseDate.ToLocalTime(),
                IsArchived = movieDto.IsArchived,
                Genres = GenresListToString(movieDto.Genres),
                IsFeatured = movieDto.IsFeatured,
                Actors = PeopleListToString(movieDto.Actors),
                Directors = PeopleListToString(movieDto.Directors),
                Studios = StudiosListToString(movieDto.Studios)
            };
        }

        public LocationDto ToLocationDto(Location location)
        {
            List<RoomDto> assignedRooms = GetAssignedRoomsForLocation(location.Id);

            return new LocationDto()
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                ZipCode = location.ZipCode,
                Country = location.Country,
                State = location.State,
                Logo = location.Logo,
                IsShutdown = location.IsShutdown,
                Rooms = assignedRooms
            };
        }

        public List<RoomDto> GetAssignedRoomsForLocation(int locationId)
        {
            List<RoomDto> assignedRooms = new List<RoomDto>();
            RoomAssignment roomAssignment = locationRepository.GetRoomAssignmentForLocation(locationId);
            if (roomAssignment != null)
            {
                string[] roomIdsRaw = roomAssignment.RoomIds.Split(';');
                foreach (string roomIdRaw in roomIdsRaw)
                {
                    if (int.TryParse(roomIdRaw, out int roomId))
                    {
                        RoomDto room = ToRoomDto(roomRepository.GetRoomById(roomId));
                        if (room != null)
                        {
                            assignedRooms.Add(room);
                        }
                    }
                }
            }

            return assignedRooms.OrderBy(x => x.Name).ToList();
        }

        public Location ToLocation(LocationDto locationDto)
        {
            return new Location()
            {
                Id = locationDto.Id,
                Name = locationDto.Name,
                Address = locationDto.Address,
                ZipCode = locationDto.ZipCode,
                Country = locationDto.Country,
                State = locationDto.State,
                Logo = locationDto.Logo,
                IsShutdown = locationDto.IsShutdown
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

        private List<PersonDto> PeopleStringToList(string peopleString)
        {
            List<PersonDto> people = new List<PersonDto>();
            if (peopleString == null)
            {
                return people;
            }

            string[] peopleIds = peopleString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in peopleIds)
            {
                people.Add(ToPersonDto(movieRepository.GetPersonById(int.Parse(id))));
            }

            return people;
        }

        private string PeopleListToString(ICollection<PersonDto> people)
        {
            string peopleString = "";

            foreach (PersonDto person in people)
            {
                peopleString += peopleString == "" ? person.Id.ToString() : ";" + person.Id;
            }

            return peopleString;
        }

        private List<StudioDto> StudiosStringToList(string studioString)
        {
            List<StudioDto> studios = new List<StudioDto>();
            if (studioString == null)
            {
                return studios;
            }

            string[] peopleIds = studioString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in peopleIds)
            {
                studios.Add(ToStudioDto(movieRepository.GetStudioById(int.Parse(id))));
            }

            return studios;
        }

        private string StudiosListToString(ICollection<StudioDto> studios)
        {
            string studiosString = "";

            foreach (StudioDto person in studios)
            {
                studiosString += studiosString == "" ? person.Id.ToString() : ";" + person.Id;
            }

            return studiosString;
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
            ICollection<Reservation> reservationsDb = reservationRepository.GetReservations();

            foreach (string id in reservationIds)
            {
                Reservation target = reservationsDb.FirstOrDefault(x => x.Id == int.Parse(id));
                if (target != null)
                {
                    reservations.Add(ToReservationDto(target));
                }
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

        public Reservation ToReservation(ReservationDto reservationDto)
        {
            return new Reservation()
            {
                BookingDate = reservationDto.BookingDate.ToLocalTime(),
                Id = reservationDto.Id,
                ReservationStatus = reservationDto.ReservationStatus,
                RoomId = reservationDto.RoomId,
                ScheduleSlotId = reservationDto.ScheduleSlotId,
                SeatId = reservationDto.SeatId,
                UserId = reservationDto.UserId,
                Email = reservationDto.Email,
                IsConfirmed = reservationDto.IsConfirmed,
                ReservationNumber = reservationDto.ReservationNumber
            };
        }

        public ReservationDto ToReservationDto(Reservation reservation)
        {
            if (reservation == null)
            {
                return null;
            }

            return new ReservationDto()
            {
                BookingDate = reservation.BookingDate,
                Id = reservation.Id,
                ReservationStatus = reservation.ReservationStatus,
                RoomId = reservation.RoomId,
                //Room = ToRoomDto(roomRepository.GetRoomById(reservation.RoomId)),
                ScheduleSlotId = reservation.ScheduleSlotId,
                //ScheduleSlot = ToScheduleSlotDto(scheduleRepository.GetScheduleSlotById(reservation.ScheduleSlotId)),
                SeatId = reservation.SeatId,
                UserId = reservation.UserId,
                Email = reservation.Email,
                IsConfirmed = reservation.IsConfirmed,
                ReservationNumber = reservation.ReservationNumber
            };
        }

        public PersonDto ToPersonDto(Person person)
        {
            return new PersonDto()
            {
                Id = person.Id,
                Name = person.Name
            };
        }

        public Person ToPerson(PersonDto personDto)
        {
            return new Person()
            {
                Id = personDto.Id,
                Name = personDto.Name
            };
        }

        public StudioDto ToStudioDto(Studio studio)
        {
            return new StudioDto()
            {
                Id = studio.Id,
                Name = studio.Name
            };
        }

        public Studio ToStudio(StudioDto studioDto)
        {
            return new Studio()
            {
                Id = studioDto.Id,
                Name = studioDto.Name
            };
        }
        #endregion
        #endregion
    }
}
