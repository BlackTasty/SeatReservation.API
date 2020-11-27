using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class PersonSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Person> people)
        {
            if (people?.Count > 0 && databaseContext.People.Count() <= 0)
            {
                foreach (var person in people.ToList())
                {
                    databaseContext.People.Add(person);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
