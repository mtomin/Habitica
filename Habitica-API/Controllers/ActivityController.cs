using Habitica_API.Models;
using Habitica_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Habitica_API.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    [Authorize]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpGet]
        public ObjectResult GetUserActivities()
        {
            int userID = int.Parse(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var userActivities = _activityRepository.GetUserActivities(userID);
            if (userActivities?.Count == 0)
            {
                return BadRequest("Activities not found!");
            }
            return Ok(userActivities);
        }

        [HttpPut]
        public ObjectResult UpdateUserActivity(ActivityConfiguration activityConfiguration)
        {
            var userID= int.Parse(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            try
            {
                var result = _activityRepository.UpdateActivity(activityConfiguration, userID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error updating activity!");
            }
        }

        [HttpGet]
        public ObjectResult GetActivityHistory(int id)
        {
            var userID = int.Parse(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            try
            {
                var result = _activityRepository.GetActivityHistory(id, userID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error fetching activity history!");
            }
        }
    }
}