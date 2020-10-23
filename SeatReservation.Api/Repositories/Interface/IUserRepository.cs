using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Interface
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);

        bool Add(User user);

        bool Update(User user);

        bool Delete(int userId);

        bool SetPermissions(UserPermission userPermission);

        ICollection<Permission> GetPermissions(int userId);

        ICollection<Permission> GetAvailablePermissions();

        ICollection<User> Get();

        User GetById(int userId);

        Permission GetPermissionById(int id);
    }
}
