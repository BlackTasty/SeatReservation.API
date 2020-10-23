using AutoMapper;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Services.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Mapping
{
    public class DatabaseProfile : Profile
    {
        public DatabaseProfile()
        {
            CreateMap<Genre, GenreDto>()
                .ReverseMap();

            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Reservation, ReservationDto>()
                .ReverseMap();

            CreateMap<ScheduleSlot, ScheduleSlotDto>()
                .ReverseMap();

            CreateMap<SeatType, SeatTypeDto>()
                .ReverseMap();

            CreateMap<SeatPosition, SeatPositionDto>()
                .ReverseMap();
        }
    }
}
