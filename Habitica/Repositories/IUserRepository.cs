using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica.Repositories
{
    public interface IUserRepository
    {
        UserData GetUser(string userId);

        UserData UpdateUser(UserData userData);
    }
}
