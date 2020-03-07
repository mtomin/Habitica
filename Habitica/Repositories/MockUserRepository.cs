using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        private List<UserData> users = new List<UserData>()
        { new UserData()
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

        public UserData GetUser(string userId)
        {
            try
            {
                return users[int.Parse(userId)];
            }
            catch
            { return null; }
        }

        public UserData UpdateUser(UserData userData)
        {
            try
            {
                users.First(u=>u.UserId.Equals(userData.UserId, StringComparison.OrdinalIgnoreCase)).Activities[0].Name = "User has been modified";
                return users.FirstOrDefault(u => u.UserId.Equals(userData.UserId, StringComparison.OrdinalIgnoreCase));
            }
            catch
            { return null; }
        }
    }
}
