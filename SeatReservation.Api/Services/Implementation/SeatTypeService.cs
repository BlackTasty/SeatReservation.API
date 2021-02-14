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
        private readonly IParser parser;

        public SeatTypeService(ISeatTypeRepository seatTypeRepository, IMapper mapper, IParser parser)
        {
            this.seatTypeRepository = seatTypeRepository;
            this.mapper = mapper;
            this.parser = parser;
        }

        public Result AddSeatType(SeatTypeDto seatTypeDto)
        {
            return seatTypeRepository.AddSeatType(parser.ToSeatType(seatTypeDto));
        }

        public ICollection<SeatTypeDto> GetSeatTypes()
        {
            List<SeatTypeDto> seatTypes = new List<SeatTypeDto>();

            foreach (SeatType seatType in seatTypeRepository.GetSeatTypes())
            {
                seatTypes.Add(parser.ToSeatTypeDto(seatType));
            }

            return seatTypes;
        }

        public Result RemoveSeatType(int seatTypeId)
        {
            return seatTypeRepository.RemoveSeatType(seatTypeId);
        }

        public Result UpdateSeatType(SeatTypeDto seatTypeDto)
        {
            return seatTypeRepository.UpdateSeatType(parser.ToSeatType(seatTypeDto));
        }
    }
}
