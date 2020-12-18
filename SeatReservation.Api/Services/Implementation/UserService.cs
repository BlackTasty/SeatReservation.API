using AutoMapper;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public int Add(UserDto user)
        {
            return userRepository.Add(ToUser(user));
        }

        public UserDto Authenticate(string username, string password)
        {
            User user = userRepository.Authenticate(username, password);

            if (user == null)
            {
                return null;
            }
            
            return ToUserDto(user, false);
        }

        public bool Delete(int userId)
        {
            return userRepository.Delete(userId);
        }

        public ICollection<UserDto> Get()
        {
            List<UserDto> users = new List<UserDto>();
            foreach (User user in userRepository.Get())
            {
                users.Add(ToUserDto(user, false));
            }

            return users;
        }

        public ICollection<PermissionDto> GetAvailablePermissions()
        {
            return mapper.Map<ICollection<PermissionDto>>(userRepository.GetAvailablePermissions());
        }

        public UserDto GetById(int userId)
        {
            UserDto user = ToUserDto(userRepository.GetById(userId), false);
            user.Permissions = GetPermissions(userId);
            return user;
        }

        public ICollection<PermissionDto> GetPermissions(int userId)
        {
            return mapper.Map<ICollection<PermissionDto>>(userRepository.GetPermissions(userId));
        }

        public bool SetPermissions(UserPermissionDto userPermission)
        {
            return userRepository.SetPermissions(userPermission);
        }

        public bool Update(UserDto user)
        {
            return userRepository.Update(ToUser(user));
        }

        public PermissionDto GetPermissionById(int id)
        {
            return mapper.Map<PermissionDto>(userRepository.GetPermissionById(id));
        }

        private UserDto ToUserDto(User user, bool sendPassword)
        {
            return new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Password = sendPassword ? user.Password : null,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegisterDate = user.RegisterDate,
                Permissions = PermissionsStringToList(user.Permissions),
                Phone = user.Phone,
                Address = user.Address,
                PostalCode = user.PostalCode,
                State = user.State,
                Country = user.Country
            };
        }

        private User ToUser(UserDto userDto)
        {
            return new User()
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                RegisterDate = userDto.RegisterDate.ToLocalTime(),
                Permissions = PermissionsListToString(userDto.Permissions),
                Phone = userDto.Phone,
                Address = userDto.Address,
                PostalCode = userDto.PostalCode,
                State = userDto.State,
                Country = userDto.Country
            };
        }

        private List<PermissionDto> PermissionsStringToList(string permissionsString)
        {
            List<PermissionDto> permissions = new List<PermissionDto>();
            string[] permissionIds = permissionsString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in permissionIds)
            {
                permissions.Add(GetPermissionById(int.Parse(id)));
            }

            return permissions;
        }

        private string PermissionsListToString(ICollection<PermissionDto> permissions)
        {
            string permissionsString = "";

            foreach (PermissionDto permission in permissions)
            {
                permissionsString += permissionsString == "" ? permission.Id.ToString() : ";" + permission.Id;
            }

            return permissionsString;
        }
    }
}
