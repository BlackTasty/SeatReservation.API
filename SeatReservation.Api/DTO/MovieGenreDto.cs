namespace SeatReservation.Api.DTO
{
    public class MovieGenreDto
    {
        public int MovieId { get; set; }

        public MovieDto Movie { get; set; }

        public int GenreId { get; set; }

        public GenreDto Genre { get; set; }
    }
}