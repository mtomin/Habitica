using Habitica_API.DataAccess;
using Habitica_API.Helpers;
using Habitica_API.Models;
using Habitica_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Habitica_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DatabaseContext _context;
        private IRegisteredUserRepository _registeredUserRepository;

        public AuthController(DatabaseContext context, IRegisteredUserRepository registeredUserRepository)
        {
            _context = context;
            _registeredUserRepository = registeredUserRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public ObjectResult Login(LoginData loginData)
        {
            var registeredUser = _registeredUserRepository.GetUserWithCredetials(loginData);
            if (registeredUser != null)
            {
                registeredUser.User.AuthToken = TokenHelper.GenerateToken(registeredUser);
                registeredUser.User.DisplayName = registeredUser.Username;
                return Ok(registeredUser.User);
            }

            return BadRequest("Invalid username/password");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(LoginData loginData)
        {
            if (_context.RegisteredUsers.Any(ru => ru.Username.Equals(loginData.Username)))
            {
                return BadRequest("Username already taken!");
            }

            if (loginData?.Username == null || loginData?.Username == null)
            {
                return BadRequest("Username and/or password not provided!");
            }

            try
            {
                _registeredUserRepository.CreateUser(loginData);
                return Ok();
            }
            catch
            {
                return BadRequest("User creation failed!");
            }
        }
    }
}