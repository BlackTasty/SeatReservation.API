using SeatReservation.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Interface
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);

        bool Add(UserDto user);

        bool Update(UserDto user);

        bool Delete(int userId);

        bool SetPermissions(UserPermissionDto userPermission);

        ICollection<PermissionDto> GetPermissions(int userId);

        ICollection<PermissionDto> GetAvailablePermissions();

        ICollection<UserDto> Get();

        UserDto GetById(int userId);

        PermissionDto GetPermissionById(int id);
    }
}
