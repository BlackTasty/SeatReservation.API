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
using Serilog;

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

        public ICollection<DateTime> GetDatesWithMovies()
        {
            return scheduleRepository.GetDatesWithMovies();
        }

        public Result CopySchedule(ScheduleCopyTargetDto scheduleCopyTarget)
        {
            if (scheduleCopyTarget.ScheduleSlotIds == null)
            {
                return new Result();
            }
            int lastCopyTargetId = scheduleCopyTarget.ScheduleSlotIds.Last();
            for (int i = 0; i < scheduleCopyTarget.ScheduleSlotIds.Count; i++)
            {
                ScheduleSlotDto original = GetScheduleSlotById(scheduleCopyTarget.ScheduleSlotIds.ElementAt(i));
                ScheduleSlotDto scheduleSlot = new ScheduleSlotDto()
                {
                    MovieId = original.MovieId,
                    ScheduleId = original.ScheduleId,
                    Reservations = new List<ReservationDto>()
                };

                TimeSpan diff = scheduleCopyTarget.TargetDate.ToLocalTime().Date.Subtract(original.Start);

                scheduleSlot.Start = original.Start.Add(diff).ToLocalTime();
                scheduleSlot.End = original.End.Add(diff).ToLocalTime();

                Result result = scheduleRepository.ScheduleMovie(scheduleCopyTarget.RoomIds.ElementAt(i), parser.ToScheduleSlot(scheduleSlot));
                if (!result.Success)
                {
                    if (result.Exception == null)
                    {
                        Log.Warning("Couldn't copy schedule slot with id {0}!", scheduleSlot.Id);
                    }
                    else
                    {
                        Log.Error(result.Exception, "Exception thrown while trying to copy schedule slot with id {0}!", scheduleSlot.Id);
                    }
                }
            }

            return new Result();
        }
    }
}
