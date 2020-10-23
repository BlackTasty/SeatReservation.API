using SeatReservation.Api.Configuration;
using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class UserSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<User> users)
        {
            if (users?.Count > 0 && databaseContext.Users.Count() <= 0)
            {
                foreach (var user in users.ToList())
                {
                    databaseContext.Users.Add(user);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
