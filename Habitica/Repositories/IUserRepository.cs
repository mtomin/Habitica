using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string userId);

        User UpdateUser(User userData);

        User CreateUser(User userData);

        bool DeleteUser(string userId);
    }
}
