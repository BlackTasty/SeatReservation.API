using SeatReservation.Api.Database;
using SeatReservation.Api.DTO;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly PasswordHasher hasher;

        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            hasher = new PasswordHasher();
        }

        public int Add(User user)
        {
            try
            {
                Log.Information("Trying to add a new user (Username: " + user.Username + ").");
                ICollection<User> users = Get();
                if (users.Any(x => x.Username.ToLower() == user.Username.ToLower()))
                {
                    Log.Error("User with the username " + user.Username + " exists already!");
                    return -2;
                }

                int id = users.Count + 1;
                bool duplicateId = false;
                do
                {
                    duplicateId = users.Any(s => s.Id == id);
                    if (duplicateId)
                    {
                        id++;
                    }
                } while (duplicateId);
                user.Id = id;
                user.Password = hasher.HashPassword(user.Username, user.Password);

                databaseContext.Users.Add(user);
                databaseContext.SaveChanges();

                Log.Information("New user added successfully.");
                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during user creation!");
                return -1;
            }
        }

        public User Authenticate(string username, string password)
        {
            return databaseContext.Users.FirstOrDefault(u => u.Username == username && hasher.CheckPassword(username, password, u.Password));
        }

        public bool Delete(int userId)
        {
            try
            {
                Log.Information("Trying to delete user with id " + userId + ".");
                User user = GetById(userId);

                if (user == null)
                {
                    return false;
                }

                databaseContext.Users.Remove(user);
                databaseContext.SaveChanges();

                Log.Information("User deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during user deletion!");
                return false;
            }
        }

        public ICollection<User> Get()
        {
            Log.Information("Returning all existing users.");
            return databaseContext.Users.ToList();
        }

        public ICollection<Permission> GetAvailablePermissions()
        {
            Log.Information("Returning all available permissions...");
            return databaseContext.Permissions.ToList();
        }

        public User GetById(int userId)
        {
            Log.Information("Returning user with id " + userId + ".");
            return databaseContext.Users.FirstOrDefault(u => u.Id == userId);
        }

        public ICollection<Permission> GetPermissions(int userId)
        {
            Log.Information("Returning permissions for user with id " + userId + ".");
            User user = GetById(userId);

            string[] permissionIds = user.Permissions.Split(';', StringSplitOptions.RemoveEmptyEntries);

            List<Permission> permissions = new List<Permission>();
            foreach (string permissionId in permissionIds)
            {
                permissions.Add(GetPermissionById(int.Parse(permissionId)));
            }

            return permissions;
        }

        public bool SetPermissions(UserPermissionDto userPermission)
        {
            try
            {
                User user = GetById(userPermission.UserId);

                if (user == null)
                {
                    Log.Warning("No user with id " + userPermission.UserId + " exists!");
                    return false;
                }

                Log.Information("User found, updating permissions.");
                string permissions = "";

                foreach (PermissionDto permission in userPermission.Permissions)
                {
                    permissions += permissions == "" ? permission.Id.ToString() : ";" + permission.Id;
                }

                user.Permissions = permissions;
                databaseContext.Users.Update(user);
                databaseContext.SaveChanges();

                Log.Information("Permissions applied.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred while applying permissions for user!");
                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                Log.Information("Trying to update user data for user \"" + user.Username + "\".");

                User dbUser = GetById(user.Id);
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.Username = user.Username;
                dbUser.Address = user.Address;
                dbUser.Country = user.Country;
                dbUser.Phone = user.Phone;
                dbUser.PostalCode = user.PostalCode;
                dbUser.State = user.State;
                dbUser.Email = user.Email;
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    dbUser.Password = hasher.HashPassword(user.Username, user.Password);
                }
                databaseContext.Users.Update(dbUser);
                databaseContext.SaveChanges();

                Log.Information("User data updated.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred while updating user data!");
                return false;
            }
        }

        public Permission GetPermissionById(int id)
        {
            return databaseContext.Permissions.FirstOrDefault(x => x.Id == id);
        }
    }
}
