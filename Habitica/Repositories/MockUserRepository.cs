using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> users = new List<User>()
        { new User()
        {
            UserId = (new Guid()).ToString(),
                Activities = new List<Activity>()
                {
                    new Activity()
                    {
                        Name = "Mock name",
                        Difficulty = Difficulty.Medium,
                        Description = "Mock description",
                        DoneToday = false
                    }
                },
                Experience = 11
            }
        };

        public User GetUser(string userId)
        {
            try
            {
                return users[int.Parse(userId)];
            }
            catch
            { return null; }
        }

        public User UpdateUser(User userData)
        {
            try
            {
                users.First(u => u.UserId.Equals(userData.UserId, StringComparison.OrdinalIgnoreCase)).Activities[0].Name = "User has been modified";
                return users.Find(u => u.UserId.Equals(userData.UserId, StringComparison.OrdinalIgnoreCase));
            }
            catch
            { return null; }
        }

        public User CreateUser(User userData)
        {
            try
            {
                users.Add(userData);
                return userData;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteUser(string userId)
        {
            var userToRemove = users.Find(u => u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
            if (userToRemove == null)
                return false;
            try
            {
                users.Remove(userToRemove);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
