using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        public UserData GetUser(string userId)
        {
            return new UserData()
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
            };
        }
    }
}
