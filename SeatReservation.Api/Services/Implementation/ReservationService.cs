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
        private readonly IMapper mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }

        public Result AddReservation(ReservationDto reservation)
        {
            return reservationRepository.AddReservation(mapper.Map<Reservation>(reservation));
        }

        public Result CancelReservation(int reservationId)
        {
            return reservationRepository.CancelReservation(reservationId);
        }

        public ReservationDto GetById(int id)
        {
            return mapper.Map<ReservationDto>(reservationRepository.GetById(id));
        }

        public ICollection<ReservationDto> GetReservations()
        {
            return mapper.Map<ICollection<ReservationDto>>(reservationRepository.GetReservations());
        }
    }
}
