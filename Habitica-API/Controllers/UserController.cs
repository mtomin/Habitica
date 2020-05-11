using Habitica.Repositories;
using Habitica_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Habitica.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/{userId?}")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserRepository _userRepository;

        private readonly IActivityRepository _activityRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IActivityRepository activityRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _activityRepository = activityRepository;
        }

        //Get user details.
        [HttpGet]
        [Produces("application/json")]
        public ObjectResult GetUserDetails(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user != null)
                return Ok(user);
            else
                return BadRequest("User does not exist");
        }

        [HttpPut]
        public ObjectResult UpdateUserData(User userData)
        {
            var updatedData = _userRepository.UpdateUser(userData);
            return Ok(updatedData);
        }

        [HttpPost]
        public ObjectResult CreateUser(User userData)
        {
            var createdUser = _userRepository.CreateUser(userData);
            if (createdUser != null)
                return Ok(createdUser);
            else
                return BadRequest("User creation failed");
        }

        [HttpDelete]
        public StatusCodeResult DeleteUser(int userId)
        {
            var userDeleted = _userRepository.DeleteUser(userId);
            if (userDeleted)
                return Ok();
            else
                return BadRequest();
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