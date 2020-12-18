using AutoMapper;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Interface;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IScheduleRepository scheduleRepository;
        private readonly IRoomRepository roomRepository;
        private readonly ISeatTypeRepository seatTypeRepository;
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;
        private readonly IParser parser;

        public ReservationService(IReservationRepository reservationRepository, IScheduleRepository scheduleRepository,
            IRoomRepository roomRepository, ISeatTypeRepository seatTypeRepository, IMovieRepository movieRepository, IMapper mapper, IParser parser)
        {
            this.reservationRepository = reservationRepository;
            this.scheduleRepository = scheduleRepository;
            this.roomRepository = roomRepository;
            this.seatTypeRepository = seatTypeRepository;
            this.movieRepository = movieRepository;
            this.mapper = mapper;
            this.parser = parser;
        }

        public Result AddReservation(ICollection<ReservationDto> reservations)
        {
            List<Reservation> reservationsDb = new List<Reservation>();

            foreach (ReservationDto reservation in reservations)
            {
                reservationsDb.Add(parser.ToReservation(reservation));
            }

            return reservationRepository.AddReservation(reservationsDb);
        }

        public Result CancelReservation(int reservationId, int userId)
        {
            Reservation reservation = reservationRepository.GetById(reservationId);

            if (reservation == null)
            {
                return new Result(false);
            }

            if (reservation.ReservationStatus == ReservationStatus.Reserved) // Check reservation date if older than 30 min or the scheduled movie starts in less than 15 minues
            {
                ScheduleSlot scheduleSlot = scheduleRepository.GetScheduleSlotById(reservation.ScheduleSlotId);
                if (scheduleSlot == null)
                {
                    return new Result(false);
                }

                ReservationDto reservationDto = parser.ToReservationDto(reservation);

                //Check if reservation is older than 30 minutes or if movie start is less than 15 minutes away. If so deny the cancellation
                if (DateTime.Now.Subtract(reservation.BookingDate).TotalMinutes < 30 || scheduleSlot.Start.Subtract(DateTime.Now).TotalMinutes < 15)
                {
                    return new Result(false);
                }
            }

            return reservationRepository.CancelReservation(reservation, userId);
        }

        public bool CanCancelReservation(int reservationId)
        {
            Reservation reservation = reservationRepository.GetById(reservationId);

            if (reservation.ReservationStatus == ReservationStatus.Sold) // Check reservation date if older than 30 min or the scheduled movie starts in less than 15 minues
            {
                ScheduleSlot scheduleSlot = scheduleRepository.GetScheduleSlotById(reservation.ScheduleSlotId);
                if (scheduleSlot == null)
                {
                    return false;
                }

                ReservationDto reservationDto = parser.ToReservationDto(reservation);

                //Check if reservation is older than 30 minutes or if movie start is less than 15 minutes away. If so deny the cancellation
                if (DateTime.Now.Subtract(reservation.BookingDate).TotalMinutes > 30 || scheduleSlot.Start.Subtract(DateTime.Now).TotalMinutes < 15)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public ReservationDto GetById(int id)
        {
            return parser.ToReservationDto(reservationRepository.GetById(id));
        }

        public ICollection<ReservationDto> GetReservations()
        {
            List<ReservationDto> reservations = new List<ReservationDto>();
            foreach (Reservation reservation in reservationRepository.GetReservations())
            {
                reservations.Add(parser.ToReservationDto(reservation));
            }
            return reservations;
        }

        public ICollection<ReservationDto> GetReservationsForSchedule(int scheduleId)
        {
            List<ReservationDto> reservations = new List<ReservationDto>();
            foreach (Reservation reservation in reservationRepository.GetReservationsForSchedule(scheduleId))
            {
                reservations.Add(parser.ToReservationDto(reservation));
            }

            return reservations;
        }

        public ICollection<ReservationBundleDto> GetReservationsForUserId(int userId, bool getReservationHistory)
        {
            List<ReservationBundleDto> reservationBundles = new List<ReservationBundleDto>();

            foreach (var group in reservationRepository.GetReservationsForUserId(userId, getReservationHistory)
                                    .GroupBy(x => x.ReservationNumber))
            {
                List<ReservationDto> reservations = new List<ReservationDto>();
                foreach (Reservation reservation in group)
                {
                    reservations.Add(parser.ToReservationDto(reservation));
                }

                ReservationBundleDto reservationBundle = new ReservationBundleDto();
                reservationBundle.Reservations = reservations;
                reservationBundle.ReservationNumber = reservations[0].ReservationNumber;
                reservationBundle.ReservationData = GetReservationData(reservations);
                reservationBundle.ScheduleSlot = parser.ToScheduleSlotDto(scheduleRepository.GetScheduleSlotById(reservations[0].ScheduleSlotId));
                reservationBundle.Movie = parser.ToMovieDto(movieRepository.GetById(reservationBundle.ScheduleSlot.MovieId));
                reservationBundle.CanCancel = CanCancelReservation(reservations[0].Id);

                reservationBundles.Add(reservationBundle);
            }


            /*List<ReservationDto> reservations = new List<ReservationDto>();
            foreach (Reservation reservation in reservationRepository.GetReservationsForUserId(userId, getReservationHistory))
            {
                reservations.Add(parser.ToReservationDto(reservation));
            }
            */
            return reservationBundles;
        }

        public ReservationDataDto GetReservationData(ICollection<ReservationDto> reservations)
        {
            ReservationDataDto reservationData = new ReservationDataDto();
            Room room = roomRepository.GetRoomById(reservations.ElementAt(0).RoomId);
            reservationData.Room = parser.ToRoomDto(room);
            reservationData.RoomTechnology = mapper.Map<RoomTechnologyDto>(roomRepository.GetTechnologyById(room.TechnologyId));
            reservationData.SeatPositions = new List<SeatPositionDto>();
            foreach(ReservationDto reservation in reservations)
            {
                SeatPositionDto seatPosition = mapper.Map<SeatPositionDto>(roomRepository.GetSeatPositionById(reservation.SeatId));
                seatPosition.SeatType = mapper.Map<SeatTypeDto>(seatTypeRepository.GetSeatTypeById(seatPosition.SeatTypeId));
                reservationData.SeatPositions.Add(seatPosition);
            }

            return reservationData;
        }
    }
}
