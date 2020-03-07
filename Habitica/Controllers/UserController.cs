using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Habitica.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Habitica.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/{userId?}")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        //Get user details.
        [HttpGet]
        [Produces("application/json")]
        public ObjectResult GetUserDetails(string userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user != null)
                return Ok(user);
            else
                return BadRequest("User does not exist");
        }

        [HttpPut]
        public ObjectResult UpdateUserData(UserData userData)
        {
            var updatedData = _userRepository.UpdateUser(userData);
            return Ok(updatedData);
        }

        [HttpGet]
        public string GetStuff()
        {
            _logger.Log(LogLevel.Critical, "Some information");
            _logger.LogCritical("Testing .net CORE");
            _logger.LogDebug("Debug logging");
            Console.WriteLine("asdfg");
            return "MOCKMOCK!";
        }
    }
}