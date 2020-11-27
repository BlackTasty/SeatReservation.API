using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Banner { get; set; }

        public string Poster { get; set; }

        public string Logo { get; set; }

        public string Trailer { get; set; }

        public string Description { get; set; }

        public int MovieLength { get; set; } // In minutes

        public DateTime ReleaseDate { get; set; }

        public ICollection<ScheduleSlotDto> ScheduleSlots { get; set; }

        public bool IsArchived { get; set; }

        public ICollection<GenreDto> Genres { get; set; }

        public bool IsFeatured { get; set; }

        public ICollection<PersonDto> Directors { get; set; }

        public ICollection<PersonDto> Actors { get; set; }

        public ICollection<StudioDto> Studios { get; set; }

        //TODO: Add Rating?
    }
}
