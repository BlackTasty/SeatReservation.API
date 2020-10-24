using AutoMapper;
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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IRoomService roomService;
        private readonly IMapper mapper;
        private readonly IParser parser;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, IParser parser)
        {
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
            this.parser = parser;
        }

        public ICollection<ScheduleDto> GetSchedules()
        {
            List<ScheduleDto> schedules = new List<ScheduleDto>();

            foreach (Schedule schedule in scheduleRepository.GetSchedules())
            {
                schedules.Add(parser.ToScheduleDto(schedule));
            }

            return schedules;
        }

        public Result RemoveScheduledMovie(int roomId, int scheduleSlotId)
        {
            return scheduleRepository.RemoveScheduledMovie(roomId, scheduleSlotId);
        }

        public Result ScheduleMovie(int roomId, ScheduleSlotDto scheduleSlot)
        {
            return scheduleRepository.ScheduleMovie(roomId, parser.ToScheduleSlot(scheduleSlot));
        }

        public Result AddSchedule(ScheduleDto schedule)
        {
            return scheduleRepository.AddSchedule(parser.ToSchedule(schedule));
        }

        public Result RemoveSchedule(int scheduleId)
        {
            return scheduleRepository.RemoveSchedule(scheduleId);
        }

        public int GenerateSchedule(bool writeToDatabase)
        {
            return scheduleRepository.GenerateSchedule(writeToDatabase);
        }

        public ScheduleSlotDto GetScheduleSlotById(int slotId)
        {
            return parser.ToScheduleSlotDto(scheduleRepository.GetScheduleSlotById(slotId));
        }
    }
}
