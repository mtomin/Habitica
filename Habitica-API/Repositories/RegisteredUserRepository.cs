using Habitica_API.DataAccess;
using Habitica_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Habitica_API.Repositories
{
    public class RegisteredUserRepository : IRegisteredUserRepository
    {
        private DatabaseContext _context;

        public RegisteredUserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void CreateUser(LoginData loginData)
        {
            RegisteredUser newUser = new RegisteredUser()
            {
                Username = loginData.Username,
                User = new Habitica.User()
                {
                    ActivityData = GetDefaultActivities()
                }
            };

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(loginData.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            newUser.PasswordHash = Convert.ToBase64String(hashBytes);
            _context.RegisteredUsers.Add(newUser);
            _context.SaveChanges();

        }

        public void DeleteUser()
        {
            throw new NotImplementedException();
        }

        public RegisteredUser GetUserWithCredetials(LoginData loginData)
        {
            var registeredUser = _context.RegisteredUsers.FirstOrDefault(u => u.Username.Equals(loginData.Username));

            if (registeredUser == null)
            {
                return null;
            }

            string correctPasswordHash = registeredUser?.PasswordHash;

            if (String.IsNullOrEmpty(correctPasswordHash))
            {
                return null;
            }

            byte[] hashBytes = Convert.FromBase64String(correctPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(loginData.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return null;
            }

            return registeredUser;
        }

        private List<ActivityConfiguration> GetDefaultActivities()
        {
            var activityConfigurations = new List<ActivityConfiguration>();
            foreach (var activity in _context.Activities.Where(a => a.IsDefault))
            {
                activityConfigurations.Add(new ActivityConfiguration()
                {
                    Activity = activity,
                    Difficulty = Habitica.Difficulty.Medium,
                    DoneToday = false,
                    Goal = Goal.Weekly
                });
            }

            return activityConfigurations;
        }
    }
}
