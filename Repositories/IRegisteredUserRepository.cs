using Habitica_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica_API.Repositories
{
    public interface IRegisteredUserRepository
    {
        public void CreateUser(LoginData loginData);

        public void DeleteUser();

        RegisteredUser GetUserWithCredetials(LoginData loginData);
    }
}
