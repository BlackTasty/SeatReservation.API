﻿using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Util
{
    public interface IParser
    {
        Schedule ToSchedule(ScheduleDto scheduleDto);

        ScheduleDto ToScheduleDto(Schedule schedule);

        ScheduleSlot ToScheduleSlot(ScheduleSlotDto scheduleSlotDto);

        ScheduleSlotDto ToScheduleSlotDto(ScheduleSlot scheduleSlot);

        Room ToRoom(RoomDto roomDto);

        RoomDto ToRoomDto(Room room);

        RoomPlan ToRoomPlan(RoomPlanDto roomPlanDto, bool includeZeroIds);

        RoomPlanDto ToRoomPlanDto(RoomPlan roomPlan);

        //RoomTechnology ToRoomTechnology(RoomTechnologyDto roomTechnologyDto);

        //RoomTechnologyDto ToRoomTechnologyDto(RoomTechnology roomTechnology);

        MovieDto ToMovieDto(Movie movie);

        Movie ToMovie(MovieDto movieDto);

        LocationDto ToLocationDto(Location location);

        Location ToLocation(LocationDto locationDto);

        List<RoomDto> GetAssignedRoomsForLocation(int locationId);

        Reservation ToReservation(ReservationDto reservationDto);

        ReservationDto ToReservationDto(Reservation reservation);

        PersonDto ToPersonDto(Person person);

        Person ToPerson(PersonDto personDto);

        StudioDto ToStudioDto(Studio studio);

        Studio ToStudio(StudioDto studioDto);
    }
}
