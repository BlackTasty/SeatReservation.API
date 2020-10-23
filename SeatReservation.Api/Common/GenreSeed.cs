using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class GenreSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Genre> genres)
        {
            if (genres?.Count > 0 && databaseContext.Genres.Count() <= 0)
            {
                foreach (var genre in genres.ToList())
                {
                    databaseContext.Genres.Add(genre);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
