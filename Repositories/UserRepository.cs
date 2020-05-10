using Habitica_API.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public User GetUser(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User UpdateUser(User userData)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User userData)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
