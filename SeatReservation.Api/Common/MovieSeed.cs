using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class MovieSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Movie> movies)
        {
            if (movies?.Count > 0 && databaseContext.Movies.Count() <= 0)
            {
                foreach (var movie in movies.ToList())
                {
                    databaseContext.Movies.Add(movie);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
