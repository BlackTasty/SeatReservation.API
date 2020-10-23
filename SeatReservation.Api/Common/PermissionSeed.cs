using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Common
{
    public class PermissionSeed
    {
        public static void Seed(DatabaseContext databaseContext, ICollection<Permission> permissions)
        {
            if (permissions?.Count > 0 && databaseContext.Permissions.Count() <= 0)
            {
                foreach (var permission in permissions.ToList())
                {
                    databaseContext.Permissions.Add(permission);
                }

                databaseContext.SaveChanges();
            }
        }
    }
}
