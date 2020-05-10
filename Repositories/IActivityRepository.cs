using Habitica;
using Habitica_API.Models;
using System.Collections.Generic;

namespace Habitica_API.Repositories
{
    public interface IActivityRepository
    {
        public void CreateActivity(ActivityConfiguration activityConfiguration);

        public ActivityConfiguration UpdateActivity(ActivityConfiguration activity, int userID);

        public void DeleteActivity(int activityId);

        public List<ActivityConfiguration> GetUserActivities(int userId);
    }
}
