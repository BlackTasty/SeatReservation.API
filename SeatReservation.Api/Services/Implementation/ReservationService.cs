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
        private readonly IMapper mapper;
        private readonly IParser parser;

        public ReservationService(IReservationRepository reservationRepository, IScheduleRepository scheduleRepository, IMapper mapper, IParser parser)
        {
            this.reservationRepository = reservationRepository;
            this.scheduleRepository = scheduleRepository;
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

            if (reservation.ReservationStatus == ReservationStatus.Sold) // Check buy date if older than 30 min or the scheduled movie starts in less than 15 minues
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

        public ReservationDto GetById(int id)
        {
            return mapper.Map<ReservationDto>(reservationRepository.GetById(id));
        }

        public ICollection<ReservationDto> GetReservations()
        {
            return mapper.Map<ICollection<ReservationDto>>(reservationRepository.GetReservations());
        }

        public ICollection<ReservationDto> GetReservationsForSchedule(int scheduleId)
        {
            List<ReservationDto> reservations = new List<ReservationDto>();
            foreach (Reservation reservation in  reservationRepository.GetReservationsForSchedule(scheduleId))
            {
                reservations.Add(parser.ToReservationDto(reservation));
            }

            return reservations;
        }
    }
}
