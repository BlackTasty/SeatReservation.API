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
    public class SeatTypeService : ISeatTypeService
    {
        private readonly ISeatTypeRepository seatTypeRepository;
        private readonly IMapper mapper;

        public SeatTypeService(ISeatTypeRepository seatTypeRepository, IMapper mapper)
        {
            this.seatTypeRepository = seatTypeRepository;
            this.mapper = mapper;
        }

        public Result AddSeatType(SeatTypeDto seatTypeDto)
        {
            return seatTypeRepository.AddSeatType(mapper.Map<SeatType>(seatTypeDto));
        }

        public ICollection<SeatTypeDto> GetSeatTypes()
        {
            return mapper.Map<ICollection<SeatTypeDto>>(seatTypeRepository.GetSeatTypes());
        }

        public Result RemoveSeatType(int seatTypeId)
        {
            return seatTypeRepository.RemoveSeatType(seatTypeId);
        }

        public Result UpdateSeatType(SeatTypeDto seatTypeDto)
        {
            return seatTypeRepository.UpdateSeatType(mapper.Map<SeatType>(seatTypeDto));
        }
    }
}
