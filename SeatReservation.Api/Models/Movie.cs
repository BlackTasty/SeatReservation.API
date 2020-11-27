using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Banner { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string Trailer { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MovieLength { get; set; } // In minutes

        [Required]
        public DateTime ReleaseDate { get; set; }

        public bool IsArchived { get; set; }

        public string Genres { get; set; }

        public bool IsFeatured { get; set; }

        public string Actors { get; set; }

        public string Directors { get; set; }

        public string Studios { get; set; }

        //TODO: Add Rating?
    }
}
